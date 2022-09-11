# CUSTIS.NetCore.EF.MigrationGenerationExtensions

[![coverage report](https://git.custis.ru/pub/custis.netcore.ef.migrationgenerationextensions/badges/main/coverage.svg)](https://git.custis.ru/pub/custis.netcore.ef.migrationgenerationextensions/-/commits/main)
[![Version](https://img.shields.io/nuget/vpre/custis.netcore.ef.migrationgenerationextensions.svg)](https://www.nuget.org/packages/CUSTIS.NetCore.EF.MigrationGenerationExtensions)
[![Downloads](https://img.shields.io/nuget/dt/custis.netcore.ef.migrationgenerationextensions.svg)](https://www.nuget.org/packages/CUSTIS.NetCore.EF.MigrationGenerationExtensions)

Adds views, synonyms, stored procedures, etc. (so-called SQL objects) to the EF model. Creates migrations, when those objects are changed.
SQL objects are defined as raw SQL in C#-code or in embedded resources. They can be even generated at runtime.

All EF Core model-tracking and application features are supported:
* When SQL-objects change, migrations are generated.
* SQL-objects are applied on `Database.MigrateSafe()` or `DatabaseFacade.EnsureCreated()`.
* Correct script is generated on `dotnet ef migrations script`.
* Database is updated on `dotnet ef database update`.

# How to use

* Configure runtime services
  * Call `AddDbContextServicesExtension` and `UseSqlObjects` either in [`Startup.ConfigureServices.AddDbContext`](src/TestEntryPoint/Startup.cs) or in [`DbContext.OnConfiguring`](src/TestDataAccessLayer/TestContext.cs)
* Configure designtime services
  * Create an empty [`DbDesignTimeServices`](src/TestEntryPoint/DbDesignTimeServices.cs) in your entry point. The class should inherit from `CustomNpgsqlDesignTimeServices`
  * Call `AddDesignTimeServicesExtension` and `UseSqlObjects` in your [`DesignTimeDbContextFactory`](src/TestDataAccessLayer/DesignTimeDbContextFactory.cs)
    * If you call `AddDbContextServicesExtension` and `UseSqlObjects` in `DbContext.OnConfiguring` while configuring runtime services, then this step is not necessary
* Add SqlObjects to your context in [`DbContext.OnModelCreating`](src/TestDataAccessLayer/TestContext.cs)
* Generate migrations / scripts as usual

## Ways to add SqlObjects to the model

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    //...

    // Add SqlObject directly
    // Order is used to define the order in which objects are created / updated in DB
    const string Sql = "create or replace view migr_ext_tests.v_view_10 as select * from migr_ext_tests.my_table;";
    modelBuilder.AddSqlObjects(new SqlObject(Name: "v_view_10", SqlCode: Sql) { Order = 10 });

    // Add all embedded resources, placed in assembly's "Sql" folder
    // Only *.sql resources are added
    // There is no way to define order for embedded objects
    // Use resources' names if you need to sort objects
    modelBuilder.AddSqlObjects(assembly: typeof(Class1).Assembly, folder: "Sql");
}
```

# Known limitations
- Doesn't drop deleted objects (generates non compilable code, so the developer should write drop-code himself).
- Only C# is supported
- Only Postgres SQL is supported, but any other DB can be easily added (use [`CUSTIS.NetCore.EF.MigrationGenerationExtensions.PostgreSQL`](src/CUSTIS.NetCore.EF.MigrationGenerationExtensions.PostgreSQL) as an example)

# Testing
1. Open TestDataAccessLayer folder in terminal
2. Set connection string
  * `dotnet user-secrets set conn "***"`
3. Install dotnet-ef
  * `dotnet tool restore`
4. Use the following commands to add migrations / update DB
  * `dotnet dotnet-ef migrations add MyMigr --context TestContext --project TestDataAccessLayer.csproj --startup-project ../TestEntryPoint/TestEntryPoint.csproj`
  * `dotnet dotnet-ef database update --context TestContext --project TestDataAccessLayer.csproj --startup-project ../TestEntryPoint/TestEntryPoint.csproj`
