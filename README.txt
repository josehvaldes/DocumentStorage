 README
 Author: jose Valdes (jmurguia@gmail.com)
 Project: DocumentStorage 
 
 Installation:
 1. Clone the repository 
   https://github.com/josehvaldes/DocumentStorage.git
   
 2. Connect to a PosgreSQL server and create TWO databases. One for the DocuStorage Web API and one for Document Content
 
 3. Go to the 'Database' folder in your local repository and run the scripts on your new database in the following order:
	For the Web API 
    - DocuStorage.sql
	- DocuStorateData.sql
	
	For the Document Content
	- DocumentContent.sql
 
 4. Go to your code repository and edit the appsettings.json files. Change the ConnectionStrings value with your database information
 
	- DocuStorage\appsettings.json
	- DocuStore.Tests\appsettings.json
	- DocuStorage.Tests.Mockups\appsettings.json
	
	For the WebAPI use the "postgresql" connection string key
	For the Document Content use the  "contentdb" connection string key
 
 5. Go to DocuStorageUI folder in your repository and execute the following commands
   > npm install
 
 6. Go to DocuStorage folder  in your repository. Compile and deploy the solution.
 
 7. Open the DocuStore.Tests project and execute the unit tests for Documents, Groups, and Documents
 
 8. Run the DocuStorage and DocuStorageUI apps.
    For DocuStorageUI you may need to run
    > npm start 
	For DocuStorage deploy it as a WebApi Application
 
 9. There are two different Data Layer libraries to demonstrate how ease will be to implement a new one using a different DBMS or ORM library
     - DocuStorage.Data, uses ADO.Net directly
	 - DocuStorage.Data.Dapper, uses Dapper library
	Each data layer library has their own testing library.
	
 10. The DocuStorage.Data.Dapper library has two test fixtures. One to test the integration between the Data layer and the Database. The other to test the functionality using Moq.


Usage: 
  In the login page use the following credentials
   username: root
   password: root123

