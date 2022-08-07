# CUSTIS.NetCore.EF.MigrationGenerationExtensions.PostgreSQL

[![coverage report](https://git.custis.ru/pub/custis.netcore.ef.migrationgenerationextensions/badges/main/coverage.svg)](https://git.custis.ru/pub/custis.netcore.ef.migrationgenerationextensions/-/commits/main)

Creates and updates database objects (views, synonyms, stored procedures, etc) from SQL code.

MigrationGenerationExtensions tracks changes of views, synonyms and other SQL-objects.
SQL-objects can be stored as C#-code or as embedded resources.
The package automatically generates migrations, when new objects are added, existing updated or deleted.

# How to use

* Call `AddDbContextServicesExtension` either in [`Startup.ConfigureServices.AddDbContext`](src/TestEntryPoint/Startup.cs) or in `DbContext.OnConfiguring`
* Create an empty [`DbDesignTimeServices`](src/TestEntryPoint/DbDesignTimeServices.cs) in your entry point. The class should inherit from `CustomNpgsqlDesignTimeServices`
* Call `AddDesignTimeServicesExtension` in your [`DesignTimeDbContextFactory`](src/TestDataAccessLayer/DesignTimeDbContextFactory.cs)
    * There is an opinion, that if you call `AddDbContextServicesExtension` in `DbContext.OnConfiguring`, then `AddDesignTimeServicesExtension` in `DesignTimeDbContextFactory` is not necessary
* Add SqlObjects to your context in [`DbContext.OnModelCreating`](src/TestDataAccessLayer/TestContext.cs)
* Generate migrations as usual

## Ways to add SqlObjects to the model

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    //...

    // Add SqlObject directly
    const string Sql = "create or replace view migr_ext_tests.v_view_10 as select * from migr_ext_tests.my_table;";
    modelBuilder.AddSqlObjects(new SqlObject(Name: "v_view_10", SqlCode: Sql) { Order = 10 });

    // Add all embedded resources, placed in assembly's "Sql" folder
    // Only *.sql resources are added
    modelBuilder.AddSqlObjects(assembly: typeof(Class1).Assembly, folder: "Sql");
}
```

# Known limitations
- Doesn't drop deleted objects (generates non compilable code, so the developer should write drop-code himself).

# Testing
1. Open TestDataAccessLayer folder in terminal
2. Set connection string
  * `dotnet user-secrets set conn "***"`
3. Install dotnet-ef
  * `dotnet tool restore`
4. Use the following commands to add migrations / update DB
  * `dotnet dotnet-ef migrations add MyMigr --context TestContext --project TestDataAccessLayer.csproj --startup-project ../TestEntryPoint/TestEntryPoint.csproj`
  * `dotnet dotnet-ef database update --context TestContext --project TestDataAccessLayer.csproj --startup-project ../TestEntryPoint/TestEntryPoint.csproj`