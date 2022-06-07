using CUSTIS.NetCore.EF.MigrationGenerationExtensions.Generation.Contracts;
using CUSTIS.NetCore.EF.MigrationGenerationExtensions.SqlObjects;
using CUSTIS.NetCore.EF.MigrationGenerationExtensions.Utils;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Migrations.Design;
using Microsoft.Extensions.DependencyInjection;

namespace CUSTIS.NetCore.EF.MigrationGenerationExtensions.Generation
{
    /// <summary> Сервисы, необходимые для создания миграций </summary>
    public class CustomDesignTimeServices : IDesignTimeServices
    {
        /// <inheritdoc />
        public virtual void ConfigureDesignTimeServices(IServiceCollection services)
        {
            services.ReplaceBySingleton<IMigrationsCodeGenerator, CustomMigrationsGenerator>();
            services.ReplaceBySingleton<ICSharpMigrationOperationGenerator, CustomMigrationOperationGenerator>();
            services.ReplaceBySingleton<ICSharpSnapshotGenerator, CustomSnapshotGenerator>();

            services.AddSingleton<ICustomSnapshotGenerator, SqlObjectsSnapshotGenerator>();

            services.AddSingleton<IModelNamespaceProvider, SqlObjectsNamespaceProvider>();

            services.AddSingleton<ICustomMigrationOperationGenerator, ExecuteSqlMigrationOperationGenerator>();
        }
    }
}