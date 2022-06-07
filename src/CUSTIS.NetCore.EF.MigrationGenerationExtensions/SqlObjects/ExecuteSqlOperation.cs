using CUSTIS.NetCore.EF.MigrationGenerationExtensions.Generation;

namespace CUSTIS.NetCore.EF.MigrationGenerationExtensions.SqlObjects
{
    /// <summary> Operation that executes raw sql </summary>
    internal class ExecuteSqlOperation : CustomMigrationOperation
    {
        /// <summary> Operation that executes raw sql </summary>
        public ExecuteSqlOperation(string name, string sqlCode, int order)
        {
            Name = name;
            SqlCode = sqlCode;
            Order = order;
        }

        /// <summary> Operation that executes raw sql </summary>
        public ExecuteSqlOperation(SqlObject sqlObject)
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

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"SqlObject [{Name}]";
        }
    }
}