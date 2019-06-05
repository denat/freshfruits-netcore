# freshfruits-netcore
A sample .NET Core web app, with tests

# Installation

1. Install SQL Server 2017 Express - https://www.microsoft.com/en-us/sql-server/sql-server-editions-express
	After installing, make sure to copy the Connection String that is shown at the end. 
	Looks like this: Server=localhost\\SQLEXPRESS;Database=MyAmazingBlog;Trusted_Connection=True;MultipleActiveResultSets=true
		
2. Install SQL Server Management Studio - https://go.microsoft.com/fwlink/?linkid=2088649
3. Download and extract FreshFruits app - [LINK]
4. Open FreshFruits app (.sln file) in Visual Studio
5. Go to appsettings.json and fix your DefaultConnection string. It's most likely correct already, but you may have to change Server=localhost\\SQLEXPRESS to whatever the Server variable is in your own connection string you got in the SQL Server installation step (step 1).
6. Open Package Manager Console (Tools -> NuGet Package Manager -> Package Manager Console)
7. Run this command: Update-Database
	This runs all the migrations and creates the database + tables in your database
8. Run the app!

Test users:
	Editor user
	Username: editor@localhost
	Password: Editor123!
	
	Admin user
	Username: admin@localhost
	Password: Admin123!
