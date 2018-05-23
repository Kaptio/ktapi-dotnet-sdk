# .NET Starter Kit for KTAPI

Example ASP.NET Core 2.0 project for KTAPI integration.

## Features:

- get and store to the local MSSQL DB list of Packages from KTAPI
- get and store Package Details
- build and send Price Request

## How to run it on MacOS.

1. Install dotnet CLI and MSSQL DB
2. Clone git repository.
3. Create copy with appsettings.Development-example.json with name appsettings.Development.json and provide values for settings
4. Run `dotnet restore`
5. Run `dotnet ef database update` to init database
6. Set the environment variable `ASPNETCORE_ENVIRONMENT=Development`
7. Run `dotnet run` or `dotnet watch run` (for development)
8. Open in browser http://localhost:8081/app2.html - this is an AngularJS SPA Client. First run will take some time to get all packages from KTAPI and store them to the local DB.
