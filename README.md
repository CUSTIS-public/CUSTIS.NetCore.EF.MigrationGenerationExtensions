# CUSTIS.NetCore.EF.MigrationGenerationExtensions.PostgreSQL

[![coverage report](https://git.custis.ru/pub/custis.netcore.ef.migrationgenerationextensions/badges/main/coverage.svg)](https://git.custis.ru/pub/custis.netcore.ef.migrationgenerationextensions/-/commits/main)

Creates and updates database objects (views, synonyms, stored procedures, etc) from SQL code.

# Known limitations
- Doesn't drop deleted objects (generates non compilable code, so the developer should write drop-code himself).

# Testing
1. Open TestDataAccessLayer folder in terminal
2. Set connection string
  * dotnet user-secrets set conn "***"
3. Install dotnet-ef
  * dotnet tool restore
4. Use the following commands to add migrations / update DB
  * dotnet dotnet-ef migrations add MyMigr --context TestContext --project TestDataAccessLayer.csproj --startup-project ../TestEntryPoint/TestEntryPoint.csproj
  * dotnet dotnet-ef database update --context TestContext --project TestDataAccessLayer.csproj --startup-project ../TestEntryPoint/TestEntryPoint.csproj