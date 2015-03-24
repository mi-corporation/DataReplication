using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MiCo.MiApp.DataReplication.ClientApi;
using MiCo.MiApp.DataReplication.ClientApi.Interfaces;
using System.Net;
using System.IO;

/**
 * \cond
 */

namespace Mi_Co_DataSync_Example_App
{

    public partial class Form1 : Form
    {
        private const string ADDRESS_NOT_FOUND = "Address not found";
        private const string PHONE_NOT_FOUND = "Phone not found";
        private const string PLEASE_ENTER_CONFIG_INFO = "Please enter valid Data Replication Server configuration information...";
        private const string SYNC_FAILED = "Sorry, retrieving data from the Replication Server failed. Is the connection online?";
        private const string SYNC_COMPLETE = "Retrieving data from the Replication Server completed.";
        private const string REGISTRATION_COMPLETE = "";
        private const string REGISTRATION_FAILED = "Registration has failed.";
        private const string RESOURCE_NOT_FOUND = "The resource was not found locally. Be sure to retrieve the resource from the Replication Server first.";

        private const string MEAS_AUTH_URL = "localhost/MFS/Services/AuthServices.asmx";

        // private const string url = "http://localhost/RS/Company/resource";
        private string resourceUrl = string.Empty;
        private string baseUrl = string.Empty;

        public Form1()
        {
            InitializeComponent();
            MEASAuthServiceURL.Text = MEAS_AUTH_URL;
            MEASCustomerName.Text = "Test10";
            MEASUsername.Text = "User1";
            MEASPassword.Text = "User1";
            RSBaseURL.Text = "localhost:56658";
            RSCompanyName.Text = "Test10";
            RSResourceName.Text = "personnel";
        }

        public IEnumerable<SampleData> SampleData { get; set; }

        /// <summary>
        /// Update the baseUrl based on the RSBaseURL textbox contents.
        /// </summary>
        /// <returns>baseUrl contents</returns>
        private string updateBaseUrl() {
            var baseURL = string.Empty;

            if (string.IsNullOrEmpty(RSBaseURL.Text))
            {
                return string.Empty;
            }

            if (!RSBaseURL.Text.Contains("http"))
            {
                baseURL += "http://";
            }
            baseURL += RSBaseURL.Text;

            baseUrl = baseURL;
            return baseURL;
        }

        private string updateResourceUrl()
        {
            string resourceURL = updateBaseUrl();

            if (string.IsNullOrEmpty(resourceURL))
            {
                resourceUrl = string.Empty;
                return string.Empty;
            }

            if (string.IsNullOrEmpty(RSCompanyName.Text) || string.IsNullOrEmpty(RSResourceName.Text))
            {
                resourceUrl = string.Empty;
                return string.Empty;
            }

            resourceURL += "/" + RSCompanyName.Text + "/" + RSResourceName.Text;

            resourceUrl = resourceURL;
            return resourceURL;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // regAndSync();
            register();
        }

        public async void register()
        {

            //// Obtaining a handle to the data service
            updateBaseUrl();
            IDataAgent agent = MEAData.Agent(
                username: "user",
                baseUrl: baseUrl
            );

            //// Registering a resource in the service
            if (string.IsNullOrEmpty(updateResourceUrl()))
            {
                messages.Text = PLEASE_ENTER_CONFIG_INFO;
                return;
            }
            else
            {
                messages.Text = string.Empty;
                agent.Register(
                    name: resourceUrl,
                    url: resourceUrl,           // the url of the resource
                    registree: "TestAdventureWorks",  // a string identifying who's requesting this resource
                    poll: 1,                          // the desired frequency to poll the resource for changes, in hours
                    auth: AuthType.MiCo,               // The authentication method type
                    user: MEASUsername.Text,            // The MEAS User username
                    pass: MEASPassword.Text             // The MEAS User password
                );

                var cache = agent.Cache(resourceUrl);

                if (cache == null)
                {
                    // no such resource has been registered
                    messages.Text = REGISTRATION_FAILED;
                }
                else
                {
                    messages.Text = REGISTRATION_COMPLETE;
                }
            }
        }

        public async void regAndSync()
        {

            //// Obtaining a handle to the data service
            updateBaseUrl();
            IDataAgent agent = MEAData.Agent(
                username: "user",
                baseUrl: baseUrl
            );

            //// Registering a resource in the service

            agent.Register(
                name: resourceUrl,
                url: resourceUrl,           // the url of the resource
                registree: "MiCoTestApp",  // a string identifying who's requesting this resource
                poll: 12                          // the desired frequency to poll the resource for changes, in hours
            );

            var cache = agent.Cache(resourceUrl);

            if (cache == null)
            {
                // no such resource has been registered
            }
            else
            {

                // request that a resource be synced right now
                var response = await cache.Sync();

                if (!response.Status.IsError())
                {
                    // sync succeeded
                }
                else
                {
                    // sync failed, perhaps b/c we're offline
                }

                // perform an SQLite query against the data
                using (var conn = cache.GetDbConnection()) {
                    //var results = conn.Query<dynamic>("SELECT * FROM sqlite_master WHERE type='table'");
                    SampleData = conn.Query<SampleData>("SELECT * FROM [Entity 1]");
                }

                // fetch a resource's metainformation
                if (DateTime.UtcNow - cache.LastSyncDate > new TimeSpan(36, 0, 0))
                {        // time since last successful sync 
                    //WarnUserCacheIsStale();
                }

                if (DateTime.UtcNow - cache.LastModifiedDate > new TimeSpan(120, 0, 0))
                {        // time since data last updated on server. Always >= CacheAge.
                    //WarnUserServerDataIsOld();
                }
            }
        }

        private void deleteHalf(object sender, EventArgs e) {
            SampleData = SampleData.Where((d, i) => i % 2 == 0).ToList();
        }

        private void putBack(object sender, EventArgs e) {
            var req = WebRequest.CreateHttp(resourceUrl);

            var message = new Dictionary<string, IEnumerable<SampleData>>();

            message["Entity 1"] = this.SampleData;

            var json = JsonConvert.SerializeObject(message);

            req.Method = "PUT";
            req.Accept = "*/*";
            req.AllowAutoRedirect = false;
            req.ContentType = "application/json";

            var content = Encoding.ASCII.GetBytes(json);
            req.ContentLength = content.Length;

            using (var st = req.GetRequestStream()) {
                st.Write(content, 0, content.Length);
            }

            var resp = req.GetResponse();
        }

        private void Search_Click(object sender, EventArgs e)
        {
            //// Obtaining a handle to the data service
            if (updateBaseUrl() == string.Empty)
            {
                messages.Text = PLEASE_ENTER_CONFIG_INFO;
                return;
            }
            else
            {
                messages.Text = String.Empty;
                IDataAgent svc = MEAData.Agent(
                    username: "user",
                    baseUrl: baseUrl
                    );

                if (string.IsNullOrEmpty(updateResourceUrl()))
                {
                    messages.Text = PLEASE_ENTER_CONFIG_INFO;
                    return;
                }
                else
                {
                    messages.Text = string.Empty;
                    var res = svc.Cache(resourceUrl);


                    if (res == null)
                    {
                        // no such resource has been registered
                        messages.Text = RESOURCE_NOT_FOUND;
                    }
                    else
                    {
                        messages.Text = String.Empty;

                        // perform an SQLite query against the data
                        using (var conn = res.GetDbConnection())
                        {
                            // var results = conn.Query<dynamic>("SELECT * FROM sqlite_master WHERE type='table'");
                            // SampleData = conn.Query<SampleData>("SELECT * FROM [Entity 1]");
                            var results = conn.Query<dynamic>("SELECT * FROM [Person] where LastName LIKE '%" + SearchText.Text + "%'");

                            // listPeople.Items.Clear();

                            var dt = new DataTable();
                            dt.Columns.Add("ID");
                            dt.Columns.Add("First");
                            dt.Columns.Add("Last");

                            // Update List
                            foreach (var result in results)
                            {
                                //var item = new ListViewItem();
                                //item.Text = result.FirstName + ' ' + result.LastName;
                                //item.Tag = result.BusinessEntityID;
                                //listPeople.Items.Add(item);

                                var dr = dt.NewRow();
                                dr["First"] = result.FirstName;
                                dr["Last"] = result.LastName;
                                dr["ID"] = result.BusinessEntityID;

                                dt.Rows.Add(dr);
                            }

                            dataGridView1.DataSource = dt;
                            dataGridView1.Columns[0].Visible = false;
                        }
                    }

                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            personName.Text = string.Empty;
            personAddress.Text = string.Empty;
            personAddress2.Text = string.Empty;
            personCityStateZip.Text = string.Empty;
            personPhone.Text = string.Empty;

            if (dataGridView1.SelectedRows.Count == 0)
            { return; }

            DataTable dt = (DataTable)dataGridView1.DataSource;
            var drv = (DataRowView)dataGridView1.SelectedRows[0].DataBoundItem;
            var bizEntId = drv.Row["ID"];


            //// Obtaining a handle to the data service
            updateBaseUrl();
            IDataAgent svc = MEAData.Agent(
                username: "user",
                baseUrl: baseUrl
            );

            if (string.IsNullOrEmpty(updateResourceUrl()))
            {
                messages.Text = PLEASE_ENTER_CONFIG_INFO;
                return;
            }
            else
            {
                messages.Text = string.Empty;


                var res = svc.Cache(resourceUrl);

                if (res == null)
                {
                    // no such resource has been registered
                }
                else
                {
                    // perform an SQLite query against the data
                    using (var conn = res.GetDbConnection())
                    {
                        // Address Lookup
                        var sql = "SELECT * FROM Address " +
                            "INNER JOIN BusinessEntityAddress on Address.AddressID = BusinessEntityAddress.AddressID " +
                            "INNER JOIN StateProvince on Address.StateProvinceID = StateProvince.StateProvinceID " +
                            "where BusinessEntityAddress.BusinessEntityID = " + bizEntId;
                        var results = conn.Query<dynamic>(sql);

                        personName.Text = (string)drv.Row["First"] + " " + (string)drv.Row["Last"];

                        if (results.Count() == 0)
                        {
                            personAddress.Text = ADDRESS_NOT_FOUND;
                            return;
                        }

                        var result = results.First();

                        personAddress.Text = result.AddressLine1;
                        if (result.AddressLine2 != null)
                        {
                            personAddress2.Text = result.AddressLine2;
                        }
                        personCityStateZip.Text = result.City + ", " + result.StateProvinceCode + " " + result.PostalCode;

                        // Phone Lookup
                        sql = "SELECT * FROM PersonPhone where BusinessEntityID = " + bizEntId;
                        results = conn.Query<dynamic>(sql);

                        if (results.Count() == 0)
                        {
                            personPhone.Text = PHONE_NOT_FOUND;
                        }
                        else
                        {
                            if (results.First().PhoneNumber != null)
                            {
                                personPhone.Text = results.First().PhoneNumber;
                            }

                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            getData();
        }

        public async void getData()
        {

            //// Obtaining a handle to the data service
            updateBaseUrl();
            IDataAgent agent = MEAData.Agent(
                username: "user",
                baseUrl: baseUrl
            );

            //// Registering a resource in the service
            if (string.IsNullOrEmpty(updateResourceUrl()))
            {
                messages.Text = PLEASE_ENTER_CONFIG_INFO;
                return;
            }
            else
            {
                messages.Text = string.Empty;
                agent.Register(
                    name: resourceUrl,
                    url: resourceUrl,           // the url of the resource
                    registree: "TestAdventureWorks",  // a string identifying who's requesting this resource
                    poll: 1,                          // the desired frequency to poll the resource for changes, in hours
                    auth: AuthType.MiCo,               // The authentication method type
                    user: MEASUsername.Text,            // The MEAS User username
                    pass: MEASPassword.Text             // The MEAS User password
                );

                var cache = agent.Cache(resourceUrl);

                if (cache == null)
                {
                    // no such resource has been registered
                }
                else
                {

                    // request that a resource be synced right now
                    var response = await cache.Sync();

                    if (!response.Status.IsError())
                    {
                        // sync succeeded
                        messages.Text = SYNC_COMPLETE;
                    }
                    else
                    {
                        // sync failed, perhaps b/c we're offline
                        messages.Text = SYNC_FAILED;
                    }
                }
            }
        }
    }

    public class SampleData
    {
        public int id { get; set; }
        public string uid { get; set; }
        public string createdOn { get; set; }
        public string name { get; set; }
        public string gender { get; set; }
    }

}
/**
 * \endcond
 */
