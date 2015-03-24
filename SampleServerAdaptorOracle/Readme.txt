Readme for SampleServerAdapterOracle

SampleServerAdapterOracle is an example console application writtent to demonstrate using the APIs in
MiCo.MiApp.DataReplicationServer to load schema and data into a reference Resource in the MiCo Replication Server.

To run this example program:

1) Install or have access to a source DB schema to be imported into a MiCo Replication Server Resource.

This example was written using the Oracle Sample 'HR' database as the source database. Any database containing datatypes
that can be mapped into the supported datatypes could also be used in this example if configured correctly.

To use this example as written, install the Oracle Data Access Client then the Oracle Sample Databases.
This example was tested with ODAC 12c.
http://www.oracle.com/technetwork/topics/dotnet/utilsoft-086879.html

The Oracle Example databases contain the HR database used in this program. 
At the time of this writing, the download and installation instructions can be found at:
http://docs.oracle.com/cd/E11882_01/install.112/e24501/toc.htm#CBBCIACF

2) Configure the connection string to the source DB in App.config

3) Edit the following values in Program.cs
string MiCoReplicationServerUrl = "http://localhost:56658/";	// the base URL of the MiCo Replication Server
string companyName = "Company";									// the name of the Company used to construct the MiCo URL
string resourceName = "personnel";								// the name of the Resource to be created / imported

This example application was created to load the 'HR' database into a Resource named 'hr'
belonging to a Company named 'Company'. The Resource will be created from the source Oracle example 'HR' database
and the data will be loaded into the resource. The Company must exist on the MiCo Replication Server before this
example application can create and load data into the Resource.

3) Edit Program.cs to configure the mapping of the source DB datatypes into the MiCo Replication Server supported datatypes.

This application includes a section of code that maps the common datatypes, including those found in the 'HR' schema
into the datatypes supported by the MiCo Replication Server. You may add datatype mappings by adding mappings to the
'sqltoColumnTypes' Dictionary in the 'CreateResource' method. Your source DB schema may not contains a
datatype which can not be mapped to a supported datatype.

4) Create the Resource on the MiCo Rellication Server:
- Set the action string in Program.cs to 'PUT_SCHEMA'.
- Run the console application.

After the schema is loaded you may want to verify the result by setting the action string to 'GET_SCHEMA' and running the
application. The imported Resource schema will be displayed to the console window.

6) Load data into the Resource
- Set the action string in Program.cs to 'PUT_SCHEMA'.
- Run the console application.



