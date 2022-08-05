using CUSTIS.NetCore.EF.MigrationGenerationExtensions.Generation;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CUSTIS.NetCore.EF.MigrationGenerationExtensions.SqlObjects
{
    internal sealed class CreateOrUpdateSqlObjectSqlGenerator : CustomSqlGeneratorBase<CreateOrUpdateSqlObjectOperation>
    {
        protected override void Generate(CreateOrUpdateSqlObjectOperation operation, MigrationCommandListBuilder builder)
        {
            builder.Append(operation.SqlCode).EndCommand();
        }
    }
}