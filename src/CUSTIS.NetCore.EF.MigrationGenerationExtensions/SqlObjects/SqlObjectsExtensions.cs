using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace CUSTIS.NetCore.EF.MigrationGenerationExtensions.SqlObjects
{
    internal static class SqlObjectsExtensions
    {
        public static IOrderedEnumerable<MigrationOperation> OrderSqlObjects(
            this IReadOnlyCollection<ExecuteSqlOperation> sqlOps)
        {
            return sqlOps.OrderBy(g => g.Order).ThenBy(g => g.Name.ToUpper());
        }

        public static IOrderedEnumerable<SqlObject> OrderSqlObjects(
            this IReadOnlyCollection<SqlObject> sqlOps)
        {
            return sqlOps.OrderBy(g => g.Order).ThenBy(g => g.Name.ToUpper());
        }
    }
}