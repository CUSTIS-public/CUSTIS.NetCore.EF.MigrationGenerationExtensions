using System.Collections.Generic;
using CUSTIS.NetCore.EF.MigrationGenerationExtensions.Generation;
using CUSTIS.NetCore.EF.MigrationGenerationExtensions.SqlObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
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

            var context = new DbContext(new DbContextOptionsBuilder().UseInMemoryDatabase(nameof(CustomMigrationsModelDifferTests)).Options);

            _differ = new(
                relationalTypeMappingSource,
                Mock.Of<IMigrationsAnnotationProvider>(),
                Mock.Of<IRowIdentityMapFactory>(),
                new CommandBatchPreparerDependencies(
                    Mock.Of<IModificationCommandBatchFactory>(),
                    Mock.Of<IParameterNameGeneratorFactory>(),
                    Mock.Of<IComparer<IReadOnlyModificationCommand>>(),
                    Mock.Of<IModificationCommandFactory>(),
                    context.GetService<ILoggingOptions>(),
                    context.GetService<IDiagnosticsLogger<DbLoggerCategory.Update>>(),
                    context.GetService<IDbContextOptions>()),
                new[] { new SqlObjectsDiffer() });
        }

        [Test]
        public void GetDifferences_CorrectOrder()
        {
            //Arrange
            var view0 = new SqlObject("my_view_0", "create or replace ...") { Order = 100 };
            var view1 = new SqlObject("my_view_1", "create or replace ...") { Order = 120 };
            var view2 = new SqlObject("my_view_2", "create or replace ...");

            var view2Updated = new SqlObject("my_view_2", "updated create or replace ...") { Order = 200 };
            var view3 = new SqlObject("my_view_3", "create or replace ...") { Order = 180 };

            //Act
            var migrationOperations = _differ.GetDifferences(
                ToModel(view0, view1, view2),
                ToModel(view2Updated, view3));

            //Assert
            Assert.That(migrationOperations, Has.Exactly(4).Items);

            // DROP operations - first, with reversed order
            Assert.That(
                migrationOperations[0],
                Is.TypeOf<DropSqlObjectOperation>().With
                    .Property(nameof(DropSqlObjectOperation.Name)).EqualTo("my_view_1"));
            Assert.That(
                migrationOperations[1],
                Is.TypeOf<DropSqlObjectOperation>().With
                    .Property(nameof(DropSqlObjectOperation.Name)).EqualTo("my_view_0"));

            // CREATE or UPDATE ops - last, with direct order
            Assert.That(
                migrationOperations[2],
                Is.TypeOf<CreateOrUpdateSqlObjectOperation>().With
                    .Property(nameof(CreateOrUpdateSqlObjectOperation.Name)).EqualTo("my_view_3"));
            Assert.That(
                migrationOperations[3],
                Is.TypeOf<CreateOrUpdateSqlObjectOperation>().With
                    .Property(nameof(CreateOrUpdateSqlObjectOperation.Name)).EqualTo("my_view_2"));
        }

        private static IRelationalModel ToModel(params SqlObject[] objects)
        {
            var model = new Model(new ConventionSet())
            {
                [SqlObjectsModelExtensions.SqlObjectsData] = new List<SqlObject>(objects)
            };

            return Mock.Of<IRelationalModel>(x => x.Model == model);
        }
    }
#pragma warning restore EF1001 // Internal EF Core API usage.
}