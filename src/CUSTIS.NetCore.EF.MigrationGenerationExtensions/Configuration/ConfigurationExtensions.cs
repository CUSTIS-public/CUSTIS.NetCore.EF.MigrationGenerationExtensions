using CUSTIS.NetCore.EF.MigrationGenerationExtensions.Generation;
using CUSTIS.NetCore.EF.MigrationGenerationExtensions.Generation.Contracts;
using CUSTIS.NetCore.EF.MigrationGenerationExtensions.SqlObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;

namespace CUSTIS.NetCore.EF.MigrationGenerationExtensions.Configuration
{
    /// <summary> Конфигурация </summary>
    public static class ConfigurationExtensions
    {
        /// <summary> Добавляет сервисы, используемые во внутреннем контейнере DbContext </summary>
        internal static void AddServices(this IServiceCollection services)
        {
            services.AddSingleton<ICustomSqlGenerator, CreateOrUpdateSqlObjectSqlGenerator>();
            services.AddSingleton<ICustomSqlGenerator, DropSqlObjectSqlGenerator>();
            services.AddSingleton<IModelDiffer, SqlObjectsDiffer>();
        }

        /// <summary> Replaces services necessary for SQL objects </summary>
        public static void UseCommonSqlObjects(this DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ReplaceService<IMigrationsModelDiffer, CustomMigrationsModelDiffer>();

            ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(new SqlObjectsExtension());
        }
    }
}