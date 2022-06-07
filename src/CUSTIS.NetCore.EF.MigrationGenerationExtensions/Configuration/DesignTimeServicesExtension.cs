using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace CUSTIS.NetCore.EF.MigrationGenerationExtensions.Configuration
{
    /// <summary> Добавляет сервисы в ServiceProvider внутри DbContext</summary>
    internal class DesignTimeServicesExtension : IDbContextOptionsExtension
    {
        /// <summary> Добавляет сервисы в ServiceProvider внутри DbContext</summary>
        public void ApplyServices(IServiceCollection services)
        {
            services.AddDbContextServices();
        }

        /// <summary>
        ///     Gives the extension a chance to validate that all options in the extension are valid.
        ///     Most extensions do not have invalid combinations and so this will be a no-op.
        ///     If options are invalid, then an exception should be thrown.
        /// </summary>
        /// <param name="options"> The options being validated. </param>
        public void Validate(IDbContextOptions options)
        {
        }

        /// <summary>Information/metadata about the extension.</summary>
        public DbContextOptionsExtensionInfo Info => new DesignTimeServicesExtensionInfo(this);
    }
}