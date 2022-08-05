using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;

namespace CUSTIS.NetCore.EF.MigrationGenerationExtensions.SqlObjects
{
    /// <summary> Extensions to create operations in migrations </summary>
    public static class MigrationBuilderSqlObjectsExtensions
    {
        /// <summary> Create or update SQL object </summary>
        public static OperationBuilder<CreateOrUpdateSqlObjectOperation> CreateOrUpdateSqlObject(
            this MigrationBuilder migrationBuilder,
            string name, string sqlCode, int order)
        {
            var operation = new CreateOrUpdateSqlObjectOperation(name, sqlCode, order);
            migrationBuilder.Operations.Add(operation);

            return new OperationBuilder<CreateOrUpdateSqlObjectOperation>(operation);
        }

        /// <summary> Drop SQL object </summary>
        public static OperationBuilder<DropSqlObjectOperation> DropSqlObject(
            this MigrationBuilder migrationBuilder,
            string name, string sqlCode, int order)
        {
            var operation = new DropSqlObjectOperation(name, sqlCode, order);
            migrationBuilder.Operations.Add(operation);

            return new OperationBuilder<DropSqlObjectOperation>(operation);
        }
    }
}