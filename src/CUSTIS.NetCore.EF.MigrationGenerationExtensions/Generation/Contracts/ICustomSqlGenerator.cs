using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CUSTIS.NetCore.EF.MigrationGenerationExtensions.Generation.Contracts
{
    /// <summary> Генератор SQL-кода </summary>
    public interface ICustomSqlGenerator
    {
        /// <summary> Тип операции </summary>
        Type OperationType { get; }

        /// <summary> Генерация кода </summary>
        void Generate(CustomMigrationOperation operation, MigrationCommandListBuilder builder);
    }
}