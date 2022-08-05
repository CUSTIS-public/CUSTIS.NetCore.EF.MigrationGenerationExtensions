using CUSTIS.NetCore.EF.MigrationGenerationExtensions.Generation;

namespace CUSTIS.NetCore.EF.MigrationGenerationExtensions.SqlObjects
{
    /// <summary> Operation to drop sql object </summary>
    public class DropSqlObjectOperation : SqlObjectOperationBase
    {
        /// <summary> Operation to drop sql object </summary>
        public DropSqlObjectOperation(string name, string sqlCode, int order) : base(name, sqlCode, order)
        {
        }

        /// <summary> Operation to drop sql object </summary>
        public DropSqlObjectOperation(SqlObject sqlObject) : base(sqlObject)
        {
        }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"Drop [{Name}]";
        }
    }
}