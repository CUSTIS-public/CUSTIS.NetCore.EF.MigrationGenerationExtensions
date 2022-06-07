using System.Collections.Generic;
using System.Linq;
using CUSTIS.NetCore.EF.MigrationGenerationExtensions.Generation.Contracts;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations.Design;

namespace CUSTIS.NetCore.EF.MigrationGenerationExtensions.Generation
{
    internal sealed class CustomSnapshotGenerator : CSharpSnapshotGenerator
    {
        private readonly IReadOnlyList<ICustomSnapshotGenerator> _customSnapshotGenerators;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Microsoft.EntityFrameworkCore.Migrations.Design.CSharpSnapshotGenerator" /> class.
        /// </summary>
        public CustomSnapshotGenerator(CSharpSnapshotGeneratorDependencies dependencies, IEnumerable<ICustomSnapshotGenerator> customSnapshotGenerators) : base(dependencies)
        {
            _customSnapshotGenerators = customSnapshotGenerators.ToArray();
        }

        /// <summary>
        ///     Generates code for creating an <see cref="T:Microsoft.EntityFrameworkCore.Metadata.IModel" />.
        /// </summary>
        /// <param name="builderName"> The <see cref="T:Microsoft.EntityFrameworkCore.ModelBuilder" /> variable name. </param>
        /// <param name="model"> The model. </param>
        /// <param name="stringBuilder"> The builder code is added to. </param>
        public override void Generate(string builderName, IModel model, IndentedStringBuilder stringBuilder)
        {
            foreach (var generator in _customSnapshotGenerators)
            {
                generator.Generate(builderName, model, stringBuilder);
            }

            base.Generate(builderName, model, stringBuilder);
        }
    }
}