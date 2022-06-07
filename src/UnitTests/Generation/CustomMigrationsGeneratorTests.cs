using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using CUSTIS.NetCore.EF.MigrationGenerationExtensions.Generation;
using CUSTIS.NetCore.EF.MigrationGenerationExtensions.SqlObjects;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Design.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Design;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;
using NUnit.Framework;

namespace UnitTests.Generation
{
    [UseReporter(typeof(DiffReporter))]
    public class CustomMigrationsGeneratorTests
    {
        private CustomMigrationsGenerator _generator = null!;
        private IModel _model = null!;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
#pragma warning disable EF1001 // Internal EF Core API usage.
            var relationalTypeMappingSource = Mock.Of<IRelationalTypeMappingSource>();
            var csharpHelper = new CSharpHelper(relationalTypeMappingSource);
            var migrOpGenerator = new CustomMigrationOperationGenerator(
                new CSharpMigrationOperationGeneratorDependencies(csharpHelper),
                new[] { new ExecuteSqlMigrationOperationGenerator(csharpHelper) });

            var annotationCodeGenerator = Mock.Of<IAnnotationCodeGenerator>();

            var snapDeps = new CSharpSnapshotGeneratorDependencies(csharpHelper, relationalTypeMappingSource,
                annotationCodeGenerator);
            var snapGen = new CustomSnapshotGenerator(snapDeps, new[] { new SqlObjectsSnapshotGenerator(csharpHelper) });

            var deps = new MigrationsCodeGeneratorDependencies(relationalTypeMappingSource,
                annotationCodeGenerator);
            var csharpDeps = new CSharpMigrationsGeneratorDependencies(csharpHelper,
                migrOpGenerator, snapGen);
#pragma warning restore EF1001 // Internal EF Core API usage.
            _model = Mock.Of<IModel>();
            _generator =
                new CustomMigrationsGenerator(_model, deps, csharpDeps, new[] { new SqlObjectsNamespaceProvider() });
        }

        [Test]
        public void GenerateMigration_CorrectCode()
        {
            //Arrange
            var standardOp1 = new RenameTableOperation { Name = "T_OLD", NewName = "T_NEW" };
            var sqlOp1 = new ExecuteSqlOperation("v_view", "create v_view as select", 20);
            var sqlOp2 = new ExecuteSqlOperation("v_view_2", "create v_view_2 as select", 30);
            var standardOp2 = new RenameTableOperation { Name = "T_OLD_2", NewName = "T_NEW_2" };

            var ops = new MigrationOperation[]
            {
                standardOp1, sqlOp1, sqlOp2, standardOp2
            };

            //Act
            var migration = _generator.GenerateMigration("Custom.DataAccess", "TestMigration",
                ops, ops);

            //Assert
            Approvals.Verify(migration);
        }

        [Test]
        public void GenerateSnapshot_CorrectCode()
        {
            //Arrange
            var sqlObj1 = new SqlObject("v_view", "create v_view as select");
            var sqlObj2 = new SqlObject("v_view_2", "create v_view_2 as select") { Order = 10 };
            var sqlObj3 = new SqlObject("v_view_3", "create v_view_3 as select") { Order = 20 };

            Mock.Get(_model).Setup(m => m[SqlObjectsModelExtensions.SqlObjectsData]).Returns(new List<SqlObject> { sqlObj1, sqlObj2, sqlObj3,  });

            //Act
            var snapshot = _generator.GenerateSnapshot("Custom.DataAccess", typeof(TestContext),
                "MainDbSnapshot", _model);

            //Assert
            Approvals.Verify(snapshot);
        }
    }
}