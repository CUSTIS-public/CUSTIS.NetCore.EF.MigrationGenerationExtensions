using CUSTIS.NetCore.EF.MigrationGenerationExtensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CUSTIS.NetCore.EF.MigrationGenerationExtensions.PostgreSQL
{
    /// <summary> Extensions for using SQL objects </summary>
    public static class ConfigurationExtensions
    {
        /// <summary> Replaces some services necessary for SQL objects </summary>
        public static void UseSqlObjects(this DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseCommonSqlObjects();

            optionsBuilder.ReplaceService<IMigrationsSqlGenerator, CustomNpgsqlMigrationsSqlGenerator>();
        }
    }
}