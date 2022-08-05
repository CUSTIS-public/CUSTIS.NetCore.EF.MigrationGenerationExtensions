using CUSTIS.NetCore.EF.MigrationGenerationExtensions.Generation;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CUSTIS.NetCore.EF.MigrationGenerationExtensions.SqlObjects
{
    internal sealed class DropSqlObjectSqlGenerator : CustomSqlGeneratorBase<DropSqlObjectOperation>
    {
        protected override void Generate(DropSqlObjectOperation operation, MigrationCommandListBuilder builder)
        {
            builder.Append(operation.SqlCode).EndCommand();
        }
    }
}