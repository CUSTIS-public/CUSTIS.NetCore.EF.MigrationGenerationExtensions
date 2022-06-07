using System;
using System.Collections.Generic;
using System.Linq;
using CUSTIS.NetCore.EF.MigrationGenerationExtensions.Generation.Contracts;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CUSTIS.NetCore.EF.MigrationGenerationExtensions.SqlObjects
{
    internal class SqlObjectsNamespaceProvider : IModelNamespaceProvider
    {
        public IEnumerable<string?> GetNamespaces(IModel model)
        {
            if (model.GetSqlObjects().Any())
            {
                yield return typeof(SqlObjectsModelExtensions).Namespace;
            }
        }
    }
}