using CUSTIS.NetCore.EF.MigrationGenerationExtensions.Generation;

namespace CUSTIS.NetCore.EF.MigrationGenerationExtensions.SqlObjects
{
    /// <summary> Operation that creates or updates SQL object </summary>
    public class CreateOrUpdateSqlObjectOperation : SqlObjectOperationBase
    {
        /// <summary> Operation that creates or updates SQL object </summary>
        public CreateOrUpdateSqlObjectOperation(string name, string sqlCode, int order) : base(name, sqlCode, order)
        {
        }

        /// <summary> Operation that creates or updates SQL object </summary>
        public CreateOrUpdateSqlObjectOperation(SqlObject sqlObject) : base(sqlObject)
        {
        }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"SqlObject [{Name}]";
        }
    }
}