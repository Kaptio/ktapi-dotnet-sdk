How to run it.

1. Install dotnet cli and MSSQL DB
2. Clone git repository.
3. Create copy with appsettings.Development-example.json with name appsettings.Development.json and provide values for settings
4. Run dotnet restore
5. Run dotnet ef database update to init database
6. Run dotnet run or dotnet watch run (for development)
7. open in browser http://localhost:8081/app.html - it is a Angular SPA Client. First run will take some time to get all packages from KTAPI and store them to the local DB.