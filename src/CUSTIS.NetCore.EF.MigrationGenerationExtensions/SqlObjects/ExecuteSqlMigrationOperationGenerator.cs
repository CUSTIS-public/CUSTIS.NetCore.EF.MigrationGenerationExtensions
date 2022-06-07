using CUSTIS.NetCore.EF.MigrationGenerationExtensions.Generation;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CUSTIS.NetCore.EF.MigrationGenerationExtensions.SqlObjects
{
    internal sealed class ExecuteSqlMigrationOperationGenerator : CustomMigrationOperationGeneratorBase<ExecuteSqlOperation>
    {
        private readonly ICSharpHelper _code;

        public ExecuteSqlMigrationOperationGenerator(ICSharpHelper code)
        {
            _code = code;
        }

        /// <summary>
        ///     Generates code for an <see cref="ExecuteSqlOperation" />.
        /// </summary>
        protected override void Generate(ExecuteSqlOperation operation, IndentedStringBuilder builder)
        {
            builder.AppendLine($".{nameof(MigrationBuilder.Sql)}(");
            using (builder.Indent())
            {
                builder.Append("sql: ").Append(_code.Literal(operation.SqlCode)).Append(")");
            }
        }
    }
}