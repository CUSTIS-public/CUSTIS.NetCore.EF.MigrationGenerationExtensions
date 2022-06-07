using System.Collections.Generic;
using System.Linq;
using CUSTIS.NetCore.EF.MigrationGenerationExtensions.Generation.Contracts;
using CUSTIS.NetCore.EF.MigrationGenerationExtensions.Utils;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations.Design;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace CUSTIS.NetCore.EF.MigrationGenerationExtensions.Generation
{
    /// <summary> Генератор операций (используется при создании миграций) </summary>
    internal sealed class CustomMigrationsGenerator : CSharpMigrationsGenerator
    {
        private readonly IReadOnlyList<IModelNamespaceProvider> _namespaceProviders;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Microsoft.EntityFrameworkCore.Migrations.Design.CSharpMigrationsGenerator" /> class.
        /// </summary>
        public CustomMigrationsGenerator(IModel model, MigrationsCodeGeneratorDependencies dependencies,
            CSharpMigrationsGeneratorDependencies csharpDependencies, IEnumerable<IModelNamespaceProvider> namespaceProviders) : base(dependencies, csharpDependencies)
        {
            _namespaceProviders = namespaceProviders.ToArray();
        }

        /// <summary>
        ///     Gets the namespaces required for an <see cref="T:Microsoft.EntityFrameworkCore.Metadata.IModel" />.
        /// </summary>
        /// <param name="model"> The model. </param>
        /// <returns> The namespaces. </returns>
        protected override IEnumerable<string> GetNamespaces(IModel model)
        {
            var namespaces = _namespaceProviders.SelectMany(p => p.GetNamespaces(model));
            return base.GetNamespaces(model).Concat(namespaces.WhereNotNull());
        }

        /// <summary>
        ///     Gets the namespaces required for a list of <see cref="T:Microsoft.EntityFrameworkCore.Migrations.Operations.MigrationOperation" /> objects.
        /// </summary>
        /// <param name="operations"> The operations. </param>
        /// <returns> The namespaces. </returns>
        protected override IEnumerable<string> GetNamespaces(IEnumerable<MigrationOperation> operations)
        {
            var migrationOperations = operations as MigrationOperation[] ?? operations.ToArray();

            var namespaces = migrationOperations.OfType<CustomMigrationOperation>()
                .Select(o => o.GetType().Namespace)
                .WhereNotNull();

            return base.GetNamespaces(migrationOperations).Concat(namespaces);
        }
    }
}