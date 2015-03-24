Readme for SampleServerAdapterSQL

SampleServerAdapterSQL is an example console application writtent to demonstrate using the APIs in 
MiCo.MiApp.DataReplicationServer to load schema and data into a reference Resource in the MiCo Replication Server.

To run this example program:

1) Install or have access to a source DB schema to be imported into a MiCo Replication Server Resource.

	This example was written using the AdventureWorks2012 database as the source database. Any database containing datatypes 
	that can be mapped into the supported datatypes could also be used in this example if configured correctly.

	To use this example as written, install the the AdventureWorks2012 database in your development enviroment. 
	At the time of this writing, the download and installation instructions can be found at: 
	http://msftdbprodsamples.codeplex.com/releases/view/93587.

2) Configure the connection string to the source DB in App.config

3) Edit the following values in Program.cs
    string schemaName = "Person";									// the name of the source schema 				
    string MiCoReplicationServerUrl = "http://localhost:56658/";	// the base URL of the MiCo Replication Server
    string companyName = "Company";									// the name of the Company used to construct the MiCo URL
    string resourceName = "personnel";								// the name of the Resource to be created / imported

	This example application was created to load the Adventureworks2012.Person schema into a Resource named 'personnel' 
	belonging to a Company named 'Company'. The Resource will be created from the source AdventureWorks2012.Person schema
	and the data will be loaded into the resource. The Company must exist on the MiCo Replication Server before this 
	example application can create and load data into the Resource.

3) Edit Program.cs to configure the mapping of the source DB datatypes into the MEAS supported datatypes. 

	This application includes a section of code that maps the datatypes found in the AdventureWorks2012.Person schema 
	into the datatypes supported by the MiCo Replication Server. The AdventureWorks2012 DB Person schema contains a 
	'geography' sql type which does not map to a supported datatype. In order to use the 'Person' schema, a column with this 
	data type was dropped:      
		'ALTER table Person.Address drop column SpatialLocation'

4) Create the Resource on the MiCo Rellication Server:
	- Set the action string in Program.cs to 'PUT_SCHEMA'.
	- Run the console application. 

   After the schema is loaded you may want to verify the result by setting the action string to 'GET_SCHEMA' and running the 
   application. The imported Resource schema will be displayed to the console window.

6) Load data into the Resource 
	- Set the action string in Program.cs to 'PUT_SCHEMA'.
	- Run the console application. 

	Program.cs only loads the 1st 100 columns from each source table. To modify this, change the SQL in 
	SampleServerAdaptor.UpdateResourceData to import a different subset or all of the data.



