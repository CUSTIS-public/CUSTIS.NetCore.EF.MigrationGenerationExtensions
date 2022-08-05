using CUSTIS.NetCore.EF.MigrationGenerationExtensions.Generation;

namespace CUSTIS.NetCore.EF.MigrationGenerationExtensions.SqlObjects
{
    /// <summary> Operation with SQL object </summary>
    public class SqlObjectOperationBase : CustomMigrationOperation
    {
        /// <summary> Operation with SQL object </summary>
        public SqlObjectOperationBase(string name, string sqlCode, int order)
        {
            Name = name;
            SqlCode = sqlCode;
            Order = order;
        }

        /// <summary> Operation with SQL object </summary>
        public SqlObjectOperationBase(SqlObject sqlObject)
        {
            SqlCode = sqlObject.SqlCode;
            Name = sqlObject.Name;
            Order = sqlObject.Order;
        }

        /// <summary> Name of sql objects </summary>
        public string Name { get; }

        /// <summary> SQL code of object </summary>
        public string SqlCode { get; }

        /// <summary> Order in which object is applied </summary>
        public int Order { get; }
    }
}