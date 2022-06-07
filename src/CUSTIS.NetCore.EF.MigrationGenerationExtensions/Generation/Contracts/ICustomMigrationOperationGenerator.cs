using System;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CUSTIS.NetCore.EF.MigrationGenerationExtensions.Generation.Contracts
{
    internal interface ICustomMigrationOperationGenerator
    {
        Type OperationType { get; }

        void Generate(CustomMigrationOperation operation, IndentedStringBuilder builder);
    }
}