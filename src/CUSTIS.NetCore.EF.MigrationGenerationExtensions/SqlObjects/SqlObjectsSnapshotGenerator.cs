using System.Linq;
using CUSTIS.NetCore.EF.MigrationGenerationExtensions.Generation.Contracts;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CUSTIS.NetCore.EF.MigrationGenerationExtensions.SqlObjects
{
    internal sealed class SqlObjectsSnapshotGenerator : ICustomSnapshotGenerator
    {
        private readonly ICSharpHelper _code;

        public SqlObjectsSnapshotGenerator(ICSharpHelper code)
        {
            _code = code;
        }

        public void Generate(string builderName, IModel model, IndentedStringBuilder stringBuilder)
        {
            var objects = model.GetSqlObjects();
            if (!objects.Any())
            {
                return;
            }

            // modelBuilder.Model.SetSqlObjects(new SqlObject[]
            // {
            //    ...
            // });
            stringBuilder
                .AppendLine($"{builderName}.Model.{nameof(SqlObjectsModelExtensions.AddSqlObjects)}(new {nameof(SqlObject)}[]");
            stringBuilder.AppendLine("{");

            using (stringBuilder.Indent())
            {
                foreach (var obj in objects.OrderSqlObjects())
                {
                    stringBuilder.Append("new(")
                        .Append(_code.Literal(obj.Name)).Append(", ")
                        .Append(_code.Literal(obj.SqlCode)).AppendLine(")");
                    stringBuilder.AppendLine("{");

                    using (stringBuilder.Indent())
                    {
                        stringBuilder.Append(nameof(SqlObject.Order)).Append(" = ").AppendLine(_code.Literal(obj.Order));
                    }

                    stringBuilder.AppendLine("},");
                }
            }

            stringBuilder.AppendLine("});");
            stringBuilder.AppendLine();
        }
    }
}