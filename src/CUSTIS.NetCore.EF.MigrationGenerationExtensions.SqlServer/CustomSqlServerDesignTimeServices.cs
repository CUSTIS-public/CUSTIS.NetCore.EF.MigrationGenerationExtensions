using CUSTIS.NetCore.EF.MigrationGenerationExtensions.Configuration;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace CUSTIS.NetCore.EF.MigrationGenerationExtensions.PostgreSQL
{
    /// <summary> Services necessary to create migrations </summary>
    public class CustomSqlServerDesignTimeServices : CustomDesignTimeServices
    {
        /// <inheritdoc />
        public override void ConfigureDesignTimeServices(IServiceCollection services)
        {
            ReplaceBySingleton<IAnnotationCodeGenerator, CustomSqlServerAnnotationCodeGenerator>(services);
            base.ConfigureDesignTimeServices(services);
        }
    }
}