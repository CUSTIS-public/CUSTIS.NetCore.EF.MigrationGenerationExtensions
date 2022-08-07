using CUSTIS.NetCore.EF.MigrationGenerationExtensions.PostgreSQL;
using CUSTIS.NetCore.EF.MigrationGenerationExtensions.SqlObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TestDataAccessLayer
{
    public class TestContext : DbContext
    {
        public TestContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("migr_ext_tests");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Class1Configuration).Assembly);

            // Add SqlObject directly
            const string Sql = "create or replace view migr_ext_tests.v_view_10 as select * from migr_ext_tests.my_table;";
            modelBuilder.AddSqlObjects(new SqlObject(Name: "v_view_10", SqlCode: Sql) { Order = 10 });

            // Add all embedded resources, placed in assembly's "Sql" folder
            // Only *.sql resources are added
            modelBuilder.AddSqlObjects(assembly: typeof(Class1).Assembly, folder: "Sql");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<TestContext>();

            var cfg = builder.Build();
            optionsBuilder.UseNpgsql(cfg["conn"]);
            optionsBuilder.UseSqlObjects();
        }
    }
}