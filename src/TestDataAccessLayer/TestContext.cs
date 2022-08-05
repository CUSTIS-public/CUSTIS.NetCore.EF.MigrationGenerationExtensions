using CUSTIS.NetCore.EF.MigrationGenerationExtensions.PostgreSQL;
using CUSTIS.NetCore.EF.MigrationGenerationExtensions.SqlObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TestDataAccessLayer
{
    public class TestContext : DbContext
    {
        /// <summary>
        ///     <para>
        ///         Initializes a new instance of the <see cref="T:Microsoft.EntityFrameworkCore.DbContext" /> class using the specified options.
        ///         The <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" /> method will still be called to allow further
        ///         configuration of the options.
        ///     </para>
        /// </summary>
        /// <param name="options">The options for this context.</param>
        public TestContext(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        ///     Override this method to further configure the model that was discovered by convention from the entity types
        ///     exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
        ///     and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <remarks>
        ///     If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        ///     then this method will not be run.
        /// </remarks>
        /// <param name="modelBuilder">
        ///     The builder being used to construct the model for this context. Databases (and other extensions) typically
        ///     define extension methods on this object that allow you to configure aspects of the model that are specific
        ///     to a given database.
        /// </param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("migr_ext_tests");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Class1Configuration).Assembly);

            modelBuilder.AddSqlObjects(typeof(Class1).Assembly, "Sql");

            modelBuilder.AddSqlObjects(new SqlObject("my_stored_proc", "stored_proc_code") { Order = 10 });
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