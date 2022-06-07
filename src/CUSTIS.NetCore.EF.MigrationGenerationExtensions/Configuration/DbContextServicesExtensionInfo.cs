using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CUSTIS.NetCore.EF.MigrationGenerationExtensions.Configuration
{
    internal class DbContextServicesExtensionInfo : DbContextOptionsExtensionInfo
    {
        /// <summary>
        ///     Creates a new <see cref="T:Microsoft.EntityFrameworkCore.Infrastructure.DbContextOptionsExtensionInfo" /> instance containing
        ///     info/metadata for the given extension.
        /// </summary>
        /// <param name="extension"> The extension. </param>
        public DbContextServicesExtensionInfo(IDbContextOptionsExtension extension) : base(extension)
        {
        }

        /// <summary>
        ///     Returns a hash code created from any options that would cause a new <see cref="T:System.IServiceProvider" />
        ///     to be needed. Most extensions do not have any such options and should return zero.
        /// </summary>
        /// <returns> A hash over options that require a new service provider when changed. </returns>
        public override long GetServiceProviderHashCode()
        {
            return 0;
        }

        /// <summary>
        ///     Populates a dictionary of information that may change between uses of the
        ///     extension such that it can be compared to a previous configuration for
        ///     this option and differences can be logged. The dictionary key should be prefixed by the
        ///     extension name. For example, <c>"SqlServer:"</c>.
        /// </summary>
        /// <param name="debugInfo"> The dictionary to populate. </param>
        public override void PopulateDebugInfo(IDictionary<string, string> debugInfo)
        {
        }

        /// <summary>
        ///     <see langword="true" /> if the extension is a database provider; <see langword="false" /> otherwise.
        /// </summary>
        public override bool IsDatabaseProvider => false;

        /// <summary>
        ///     A message fragment for logging typically containing information about
        ///     any useful non-default options that have been configured.
        /// </summary>
        public override string LogFragment => "DB context services";
    }
}