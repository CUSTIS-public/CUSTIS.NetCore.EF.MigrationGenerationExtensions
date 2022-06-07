using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CUSTIS.NetCore.EF.MigrationGenerationExtensions.Generation.Contracts
{
    internal interface IModelNamespaceProvider
    {
        /// <summary> Gets additional namespaces for migration </summary>
        IEnumerable<string?> GetNamespaces(IModel model);
    }
}