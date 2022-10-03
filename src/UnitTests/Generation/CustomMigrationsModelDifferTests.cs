using System.Collections.Generic;
using CUSTIS.NetCore.EF.MigrationGenerationExtensions.Generation;
using CUSTIS.NetCore.EF.MigrationGenerationExtensions.SqlObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Moq;
using NUnit.Framework;

namespace UnitTests.Generation
{
#pragma warning disable EF1001 // Internal EF Core API usage.
    public class CustomMigrationsModelDifferTests
    {
        private CustomMigrationsModelDiffer _differ = null!;

        [SetUp]
        public void SetUp()
        {
            var relationalTypeMappingSource = Mock.Of<IRelationalTypeMappingSource>();

            _differ = new(
                relationalTypeMappingSource, Mock.Of<IMigrationsAnnotationProvider>(),
                Mock.Of<IChangeDetector>(),
                Mock.Of<IUpdateAdapterFactory>(),
                new CommandBatchPreparerDependencies(Mock.Of<IModificationCommandBatchFactory>(),
                    Mock.Of<IParameterNameGeneratorFactory>(),
                    Mock.Of<IComparer<ModificationCommand>>(),
                    Mock.Of<IKeyValueIndexFactorySource>(),
                    Mock.Of<ILoggingOptions>(),
                    Mock.Of<IDiagnosticsLogger<DbLoggerCategory.Update>>(),
                    Mock.Of<IDbContextOptions>()),
                new[] { new SqlObjectsDiffer() });
        }

        [Test]
        public void GetDifferences_CorrectOrder()
        {
            //Arrange
            var view1 = new SqlObject("my_view_1", "create or replace ...");
            var view2 = new SqlObject("my_view_2", "create or replace ...");

            var view2Updated = new SqlObject("my_view_2", "updated create or replace ...");
            var view3 = new SqlObject("my_view_3", "create or replace ...");

            //Act
            var migrationOperations = _differ.GetDifferences(
                ToModel(view1, view2),
                ToModel(view2Updated, view3));

            //Assert
            Assert.That(migrationOperations, Has.Exactly(4).Items);
            Assert.That(
                migrationOperations[0],
                Is.TypeOf<DropSqlObjectOperation>().With
                    .Property(nameof(DropSqlObjectOperation.Name)).EqualTo("my_view_1"));
            Assert.That(
                migrationOperations[1],
                Is.TypeOf<CreateOrUpdateSqlObjectOperation>().With
                    .Property(nameof(CreateOrUpdateSqlObjectOperation.Name)).EqualTo("my_view_1"));
            Assert.That(
                migrationOperations[2],
                Is.TypeOf<CreateOrUpdateSqlObjectOperation>().With
                    .Property(nameof(CreateOrUpdateSqlObjectOperation.Name)).EqualTo("my_view_2"));
            Assert.That(
                migrationOperations[3],
                Is.TypeOf<CreateOrUpdateSqlObjectOperation>().With
                    .Property(nameof(CreateOrUpdateSqlObjectOperation.Name)).EqualTo("my_view_2"));
        }

        private static IRelationalModel ToModel(params SqlObject[] objects)
        {
            return Mock.Of<IRelationalModel>(m => m.Model[SqlObjectsModelExtensions.SqlObjectsData] == new List<SqlObject>(objects));
        }
    }
#pragma warning restore EF1001 // Internal EF Core API usage.
}