using System;
using CUSTIS.NetCore.EF.MigrationGenerationExtensions.Generation.Contracts;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CUSTIS.NetCore.EF.MigrationGenerationExtensions.Generation
{
    internal abstract class CustomSqlGeneratorBase<T> : ICustomSqlGenerator
        where T : CustomMigrationOperation
    {
        public Type OperationType => typeof(T);

        public void Generate(CustomMigrationOperation operation, MigrationCommandListBuilder builder)
        {
            if (operation is T typedOperation)
            {
                Generate(typedOperation, builder);
                return;
            }

            throw new InvalidOperationException(
                $"Ожидался тип операции {typeof(T).Name}, но получен {operation.GetType()}");
        }

        protected abstract void Generate(T operation, MigrationCommandListBuilder builder);
    }
}