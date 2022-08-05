using CUSTIS.NetCore.EF.MigrationGenerationExtensions.Generation;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CUSTIS.NetCore.EF.MigrationGenerationExtensions.SqlObjects
{
    internal sealed class CreateOrUpdateSqlObjectMigrationGenerator : CustomMigrationOperationGeneratorBase<CreateOrUpdateSqlObjectOperation>
    {
        private readonly ICSharpHelper _code;

        public CreateOrUpdateSqlObjectMigrationGenerator(ICSharpHelper code)
        {
            _code = code;
        }

        /// <summary>
        ///     Generates code for an <see cref="CreateOrUpdateSqlObjectOperation" />.
        /// </summary>
        protected override void Generate(CreateOrUpdateSqlObjectOperation operation, IndentedStringBuilder builder)
        {
            builder.AppendLine($".{nameof(MigrationBuilderSqlObjectsExtensions.CreateOrUpdateSqlObject)}(");
            using (builder.Indent())
            {
                builder.Append("name: ").Append(_code.Literal(operation.Name)).AppendLine(",");
                builder.Append("sqlCode: ").Append(_code.Literal(operation.SqlCode)).AppendLine(",");
                builder.Append("order: ").Append(_code.Literal(operation.Order)).Append(")");
            }
        }
    }
}