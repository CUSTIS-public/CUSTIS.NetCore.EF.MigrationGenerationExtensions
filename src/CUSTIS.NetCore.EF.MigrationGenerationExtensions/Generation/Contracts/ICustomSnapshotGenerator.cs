using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CUSTIS.NetCore.EF.MigrationGenerationExtensions.Generation.Contracts
{
    internal interface ICustomSnapshotGenerator
    {
        void Generate(string builderName, IModel model, IndentedStringBuilder stringBuilder);
    }
}