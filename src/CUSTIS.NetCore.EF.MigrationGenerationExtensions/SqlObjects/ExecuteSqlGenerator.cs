using CUSTIS.NetCore.EF.MigrationGenerationExtensions.Generation;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CUSTIS.NetCore.EF.MigrationGenerationExtensions.SqlObjects
{
    internal sealed class ExecuteSqlGenerator : CustomSqlGeneratorBase<ExecuteSqlOperation>
    {
        protected override void Generate(ExecuteSqlOperation operation, MigrationCommandListBuilder builder)
        {
            builder.Append(operation.SqlCode).EndCommand();
        }
    }
}