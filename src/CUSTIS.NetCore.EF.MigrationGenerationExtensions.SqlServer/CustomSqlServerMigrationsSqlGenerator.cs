using System;
using System.Collections.Generic;
using System.Linq;
using CUSTIS.NetCore.EF.MigrationGenerationExtensions.Generation;
using CUSTIS.NetCore.EF.MigrationGenerationExtensions.Generation.Contracts;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace CUSTIS.NetCore.EF.MigrationGenerationExtensions.PostgreSQL
{
    /// <summary> Генератор SQL кода </summary>
    public sealed class CustomSqlServerMigrationsSqlGenerator : SqlServerMigrationsSqlGenerator
    {
        private readonly IReadOnlyDictionary<Type, ICustomSqlGenerator> _generators;

#pragma warning disable EF1001 // Internal EF Core API usage.
        /// <summary> Генератор SQL кода </summary>
        public CustomSqlServerMigrationsSqlGenerator(MigrationsSqlGeneratorDependencies dependencies,
            IRelationalAnnotationProvider migrationsAnnotations,
            IEnumerable<ICustomSqlGenerator> sqlGenerators) : base(dependencies, migrationsAnnotations)
#pragma warning restore EF1001 // Internal EF Core API usage.
        {
            _generators = sqlGenerators.ToDictionary(g => g.OperationType);
        }

        /// <inheritdoc />
        protected override void Generate(
            MigrationOperation operation,
            IModel? model,
            MigrationCommandListBuilder builder)
        {
            if (operation is CustomMigrationOperation customOp && _generators.ContainsKey(operation.GetType()))
            {
                _generators[operation.GetType()].Generate(customOp, builder);
                return;
            }

            base.Generate(operation, model, builder);
        }
    }
}