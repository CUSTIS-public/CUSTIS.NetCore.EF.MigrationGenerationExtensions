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
        public static void AddDbContextServices(this IServiceCollection services)
        {
            services.AddSingleton<ICustomSqlGenerator, ExecuteSqlGenerator>();
            services.AddSingleton<IModelDiffer, SqlObjectsDiffer>();
        }

        /// <summary> Добавить <see cref="DbContextServicesExtension"/> </summary>
        public static void AddDbContextServicesExtension(this IDbContextOptionsBuilderInfrastructure options)
        {
            options.AddOrUpdateExtension(new DbContextServicesExtension());
        }

        /// <summary> Добавить <see cref="DesignTimeServicesExtension"/> </summary>
        public static DbContextOptions AddDesignTimeServicesExtension(this DbContextOptions options)
        {
            return options.WithExtension(new DesignTimeServicesExtension());
        }

        /// <summary> Добавить <see cref="DesignTimeServicesExtension"/> </summary>
        public static DbContextOptions<T> AddDesignTimeServicesExtension<T>(this DbContextOptions<T> options)
            where T : DbContext
        {
            return (DbContextOptions<T>)options.WithExtension(new DesignTimeServicesExtension());
        }

        /// <summary> Replaces services necessary for SQL objects </summary>
        public static void UseCommonSqlObjects(this DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ReplaceService<IMigrationsModelDiffer, CustomMigrationsModelDiffer>();
        }
    }
}