using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Oracle.DataAccess.Client;

using MiCo.MiApp.DataReplication.Messages;
using MiCo.MiApp.DataReplication.ImportApi;


namespace SampleServerAdaptorOracle {
    static class Program {
        static string DBConectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SampleSourceDatabase"].ConnectionString;
        static AuthManager authManager;

        /// <summary>
        /// This is an example program that loads the schema and data for the Oracle Sample 'HR' database.
        /// Prior to running the program, the following must be configured:
        ///     MiCoReplicationServerUrl - the URL for the MiCo.MiApp.ReplicationServer (http://localhost:9000/)
        ///     companyName- the name of the company. the conpany must exist within the MiCo ReplicationServer
        ///     resourceName - the name of the source DB schema (in this example 'Person")
        ///     action - the HTTP Request to be constructed and sent
        ///             "GET_SCHEMA" writes the json representation of the resource to the consile window
        ///             "PUT_SCHEMA" creates the resource in the MiCo.MiApp.ReplicationServer
        ///             "PUT_DATA" loads the resource data into the MiCo.MiApp.ReplicationServer
        ///     resourceName - the connection string to the source DB (in this example a local instance of AdventureWorks2012)
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args) {

            string serverUrl = "http://localhost:56658/";	// the base URL of the MiCo Replication Server
            string customerName = "Company";									// the name of the Company used to construct the MiCo URL
            string resourceName = "HR";							// the name of the Resource to be created / imported
            string userName = "SampleUser";
            string password = "password";

            string action = "PUT_DATA";

            if (args.Length > 0) {
                if (args[0] == "GET_SCHEMA")
                    action = "GET_SCHEMA";
                else if (args[0] == "PUT_SCHEMA")
                    action = "PUT_SCHEMA";
                else if (args[0] == "PUT_DATA")
                    action = "PUT_DATA";
            }

            Console.WriteLine("SampleServerAdaptorOracle");
            Uri baseUri = new Uri(serverUrl);
            authManager = new AuthManager(baseUri);
            authManager.SetCredentials(userName, password).Wait();

            // Display a source schema parsed into a MiCo Replication Server Resource. 
            // The resource is serialized into JSON for display. This method is primarily for debug
            if (action == "GET_SCHEMA") {
                Console.WriteLine("Getting schema for company: {0}, resource: {1}", customerName, resourceName);
                GetResourceSchema(customerName, resourceName, baseUri).Wait();
            }
            // Create a MiCo Replication Server Resource from source schema
            else if (action == "PUT_SCHEMA") {
                Console.WriteLine("Creating schema for company {0}, resource {1}", customerName, resourceName);
                CreateResource(customerName, resourceName, baseUri);
            }

            // Load Resource data into the  MiCo Replication Server Resource from source database
            // Currently only the 1st 100 rows from each table are loaded
            else if (action == "PUT_DATA") {
                Console.WriteLine("Importing data for company {0}, resource {1}", customerName, resourceName);
                UpdateResourceData(customerName, resourceName, baseUri);
            }
            Console.WriteLine("Press any key to close window");
            Console.ReadKey();
    
        }
        private static void CreateResource(string company, string resource, Uri serverUrl) {

            // Map datatypes from the source DB into the allowed data types
            // Boolean, DateTime, Integer, Float and String are the only supported datatypes for MiCo Replication Server Resources.
            // These data types were included to import the Oracle Sample 'HR' database
            // To use a different source DB, you may need to add datatype mappings to this section

            Dictionary<string, ColumnSchemaType> sqlToColumnTypes;
            sqlToColumnTypes = new Dictionary<string, ColumnSchemaType>();

            sqlToColumnTypes.Add("bit", ColumnSchemaType.Boolean);
            sqlToColumnTypes.Add("datetime", ColumnSchemaType.DateTime);
            sqlToColumnTypes.Add("int", ColumnSchemaType.Integer);
            sqlToColumnTypes.Add("bigint", ColumnSchemaType.Integer);
            sqlToColumnTypes.Add("float", ColumnSchemaType.Float);
            sqlToColumnTypes.Add("nchar", ColumnSchemaType.String);
            sqlToColumnTypes.Add("nvarchar", ColumnSchemaType.String);
            sqlToColumnTypes.Add("varchar", ColumnSchemaType.String);
            sqlToColumnTypes.Add("xml", ColumnSchemaType.String);
            sqlToColumnTypes.Add("uniqueidentifier", ColumnSchemaType.String);
            sqlToColumnTypes.Add("char", ColumnSchemaType.String);
            sqlToColumnTypes.Add("varchar2", ColumnSchemaType.String);

            // Oracle's 'NUMBER' datatype may be an fixed or floating point number. 
            // There is a test later in this code to set columns with fixed point NUMBERS to the Integer datatype.
            // Mapping a number to float will support up to 64 bits when replicated into sqlite db 
            sqlToColumnTypes.Add("number", ColumnSchemaType.Float); 
            sqlToColumnTypes.Add("date", ColumnSchemaType.DateTime);

            using (var conn = new OracleConnection(DBConectionString)) {
                conn.Open();

                ResourceSchema resourceSchema = new ResourceSchema();               // the schema used to initialize the Resource
                List<TableSchema> parsedTableSchemaList = new List<TableSchema>();  // schemas created for each of the tables
                TableSchema parsedTableSchema;                                      // table schema created for a single table in the DB
                List<ColumnSchema> parsedColumnSchemaList;                          // column schema created for a single table in the DB
                ColumnSchema parsedColumnSchema;                                    // schema created for a single column
                DataTable sourceColumns;                                            // columns in a single table in the source DB

                // Get the schema of the source DB into a DataTable for parsing
                // the tables collection has 3 possible ordered restrictions: owner, table name and column name
                string[] restrictions = new string[2];
                restrictions[0] = "HR";
                DataTable sourceSchema = conn.GetSchema("Tables", restrictions);

                // each sourceTable represents a table in the source DB
                foreach (DataRow sourceTable in sourceSchema.Rows) {
#if DEBUG
                    Console.WriteLine("Table Name ==========================");
                    Console.WriteLine(sourceTable["TABLE_NAME"]);
#endif
                    parsedTableSchema = new TableSchema() { Name = sourceTable["TABLE_NAME"].ToString() };
                    parsedColumnSchemaList = new List<ColumnSchema>();
                    sourceColumns = conn.GetSchema("Columns", new string[] { "HR", sourceTable["TABLE_NAME"].ToString(), null });

                    // Get a list of the source DB table's Primary Key column names 
                    List<string> primayKeyColumnNames = GetPrimaryKeyColumnNames(conn,sourceTable["TABLE_NAME"].ToString());

                    // Create a schema for each column in the table containing the name, data type and primary key flag
                    // TODO: show error message when encountering non-matching types 
#if DEBUG
                    Console.WriteLine("Columns ===============================");
#endif
                    foreach (DataRow r in sourceColumns.Rows) {
#if DEBUG
                        if (r["DATATYPE"].ToString() == "NUMBER") {
                            Console.WriteLine("{0}: {1} ({2}, {3})", r["COLUMN_NAME"], r["DATATYPE"], r["PRECISION"], r["SCALE"]);
                        } else {
                            Console.WriteLine("{0}: {1}", r["COLUMN_NAME"], r["DATATYPE"]);
                        }
#endif
                        try {
                            // check for fixed point NUMBERS - no precision or scale
                            if (r["DATATYPE"].ToString() == "NUMBER" && (!(r["PRECISION"] is DBNull) || !(r["SCALE"] is DBNull))) {
                                parsedColumnSchema = new ColumnSchema() { Name = r["COLUMN_NAME"].ToString(), Type = ColumnSchemaType.Integer };
                            } else {
                                parsedColumnSchema = new ColumnSchema() { Name = r["COLUMN_NAME"].ToString(), Type = sqlToColumnTypes[r["DATATYPE"].ToString().ToLower()] };
                            }
                        } catch (Exception) {
                            Console.WriteLine("\n\nSchema Load Failed.");
                            Console.WriteLine("There is no mapping for Datatype: '{0}'; found in Table: '{1}', Column: '{2}' ",
                                r["DATATYPE"], sourceTable["TABLE_NAME"], r["COLUMN_NAME"]);
                            Console.WriteLine("Please add a mapping in the 'sqlToColumnTypes' Dictionary.");
                            return;
                        }
                        // mark Primary key columns
                        if (primayKeyColumnNames.Contains(r["COLUMN_NAME"].ToString())) {
#if DEBUG
                            Console.WriteLine("{0} is a Primary Key", r["COLUMN_NAME"]);
#endif
                            parsedColumnSchema.Key = true;
                        }
                        // Add created schema for column
                        parsedColumnSchemaList.Add(parsedColumnSchema);
                    }

                    // Add the list of column schemas to the table schema
                    parsedTableSchema.Columns = parsedColumnSchemaList;

                    // Add the created table schema to the list of tables
                    parsedTableSchemaList.Add(parsedTableSchema);
                }
                // Use the created list of tables schemas as the schema for the Resource to be created
                resourceSchema.Tables = parsedTableSchemaList;

                // Send the Resource as JSON to the MiCo Server to create the Resource
#if DEBUG
                var json = JsonConvert.SerializeObject(resourceSchema, Newtonsoft.Json.Formatting.Indented);
                Console.WriteLine(json);
#endif
                Console.WriteLine("Sending 'Put Schema' request to server...");
                PutResourceSchema(company, resource, resourceSchema, serverUrl).Wait();

            }
        }

        public static List<string> GetPrimaryKeyColumnNames(OracleConnection conn, string tableName)
        {
            List<string> results = new List<string>();
            string sql = @"
SELECT cols.column_name, cols.position, cons.status, cons.owner
FROM all_constraints cons, all_cons_columns cols
WHERE cols.table_name = '" + tableName + @"'
AND cons.constraint_type = 'P'
AND cons.constraint_name = cols.constraint_name
AND cons.owner = cols.owner
ORDER BY cols.table_name, cols.position";
            using (OracleCommand command = new OracleCommand(sql, conn))
            using (OracleDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    results.Add(reader.GetString(0));
                }
            }
            return results;
        }
        
        private static void UpdateResourceData(string company, string resource, Uri serverUrl) {

            using (var conn = new OracleConnection(DBConectionString)) {
                conn.Open();

                // The list of tables in the resource
                Dictionary<string, IEnumerable<Dictionary<string, object>>> tables = new Dictionary<string, IEnumerable<Dictionary<string, object>>>();

                // Get the schema of the source DB into a DataTable for parsing
                // the tables collection has 3 possible ordered restrictions: owner, table name and column name
                string[] restrictions = new string[2];
                restrictions[0] = "HR";
                DataTable sourceSchema = conn.GetSchema("Tables", restrictions);

                // each sourceTable represents a table in the source DB
                foreach (DataRow sourceTable in sourceSchema.Rows) {
                    string tableName = sourceTable["TABLE_NAME"].ToString();

                    OracleDataAdapter da = new OracleDataAdapter("Select * from " + tableName, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // The list of rows in a table
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row;

                    foreach (DataRow dr in dt.Rows) {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns) {
                            row.Add(col.ColumnName, dr[col]);
                        }
                        rows.Add(row);
                    }
                    tables.Add(tableName, rows);
                }
#if DEBUG
                var json = JsonConvert.SerializeObject(tables, Newtonsoft.Json.Formatting.Indented);
                Console.WriteLine(json);
#endif
                Console.WriteLine("Sending 'Put Data' request to server...");
                PutResourceData(company, resource, tables, serverUrl).Wait(); ;
            }
        }
        private static async Task PutResourceSchema(string company, string resource, ResourceSchema schema, Uri serverUrl) {
            using (var client = new HttpClient()) {
                client.BaseAddress = serverUrl;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                authManager.credService.AddEncryptedCredentialsToClient(client, authManager.UserName, authManager.EncryptedPassword, authManager.ClientKey);

                HttpResponseMessage response = await client.PutAsJsonAsync("_/" + company + "/" + resource, schema);
                if (response.IsSuccessStatusCode) {
                    Console.WriteLine("Schema load successful");
                } else
                    Console.WriteLine("Schema load failed");
            }
        }
        private static async Task GetResourceSchema(string companyName, string resourceName, Uri serverUrl) {
            using (var client = new HttpClient()) {
                client.BaseAddress = serverUrl;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                authManager.credService.AddEncryptedCredentialsToClient(client, authManager.UserName, authManager.EncryptedPassword, authManager.ClientKey);

                HttpResponseMessage response = await client.GetAsync("_/" + companyName + "/" + resourceName);
                if (response.IsSuccessStatusCode) {
                    ResourceSchema schema = await response.Content.ReadAsAsync<ResourceSchema>();
                    string json = JsonConvert.SerializeObject(schema, Formatting.Indented);
                    Console.WriteLine(json);
                    Console.WriteLine("Get Schema successful");

                } else
                    Console.WriteLine("Get Schema failed");
            }
        }
        private static async Task PutResourceData(string company, string resource, Dictionary<string, IEnumerable<Dictionary<string, object>>> tables, Uri serverUrl) {
            // set Timeout to 12 hours for debugging purposes
            using (var client = new HttpClient() { Timeout = new TimeSpan(12, 0, 0) }) {
                client.BaseAddress = serverUrl;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                authManager.credService.AddEncryptedCredentialsToClient(client, authManager.UserName, authManager.EncryptedPassword, authManager.ClientKey);

                HttpResponseMessage response = await client.PutAsJsonAsync("/" + company + "/" + resource, tables);
                if (response.IsSuccessStatusCode) {
                    Console.WriteLine("Data load successful");
                } else
                    Console.WriteLine("Data load failed");
            }
        }
    }
}
