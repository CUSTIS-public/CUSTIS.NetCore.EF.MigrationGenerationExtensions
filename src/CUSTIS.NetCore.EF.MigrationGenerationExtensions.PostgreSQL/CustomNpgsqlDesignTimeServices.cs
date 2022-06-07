using CUSTIS.NetCore.EF.MigrationGenerationExtensions.Generation;
using CUSTIS.NetCore.EF.MigrationGenerationExtensions.Utils;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace CUSTIS.NetCore.EF.MigrationGenerationExtensions.PostgreSQL
{
    /// <summary> Services necessary to create migrations </summary>
    public class CustomNpgsqlDesignTimeServices : CustomDesignTimeServices
    {
        /// <inheritdoc />
        public override void ConfigureDesignTimeServices(IServiceCollection services)
        {
            services.ReplaceBySingleton<IAnnotationCodeGenerator, CustomNpgsqlAnnotationCodeGenerator>();
            base.ConfigureDesignTimeServices(services);
        }
    }
}