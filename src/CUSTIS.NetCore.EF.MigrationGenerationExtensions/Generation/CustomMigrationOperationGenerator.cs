using System;
using System.Collections.Generic;
using System.Linq;
using CUSTIS.NetCore.EF.MigrationGenerationExtensions.Generation.Contracts;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations.Design;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace CUSTIS.NetCore.EF.MigrationGenerationExtensions.Generation
{
    /// <summary> Генерирует исходный код миграций </summary>
    internal sealed class CustomMigrationOperationGenerator : CSharpMigrationOperationGenerator
    {
        private readonly IReadOnlyDictionary<Type, ICustomMigrationOperationGenerator> _generators;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Microsoft.EntityFrameworkCore.Migrations.Design.CSharpMigrationOperationGenerator" /> class.
        /// </summary>
        public CustomMigrationOperationGenerator(
            CSharpMigrationOperationGeneratorDependencies dependencies,
            IEnumerable<ICustomMigrationOperationGenerator> generators) : base(dependencies)
        {
            _generators = generators.ToDictionary(g => g.OperationType);
        }

        /// <summary>
        ///     Generates code for an unknown <see cref="T:Microsoft.EntityFrameworkCore.Migrations.Operations.MigrationOperation" />.
        /// </summary>
        /// <param name="operation"> The operation. </param>
        /// <param name="builder"> The builder code is added to. </param>
        protected override void Generate(MigrationOperation operation, IndentedStringBuilder builder)
        {
            if (operation is CustomMigrationOperation customOp && _generators.ContainsKey(operation.GetType()))
            {
                _generators[operation.GetType()].Generate(customOp, builder);
                return;
            }

            base.Generate(operation, builder);
        }
    }
}