using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using CUSTIS.NetCore.EF.MigrationGenerationExtensions.Generation;
using CUSTIS.NetCore.EF.MigrationGenerationExtensions.PostgreSQL;
using CUSTIS.NetCore.EF.MigrationGenerationExtensions.SqlObjects;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Design.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Design;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace UnitTests.Generation
{
    [UseReporter(typeof(DiffReporter))]
    public class CustomMigrationsGeneratorTests
    {
        private IMigrationsCodeGenerator _generator = null!;
        private IModel _model = null!;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _model = Mock.Of<IModel>();
            var relationalTypeMappingSource = Mock.Of<IRelationalTypeMappingSource>();

            var services = new ServiceCollection();
            services.AddEntityFrameworkDesignTimeServices();
            new CustomNpgsqlDesignTimeServices().ConfigureDesignTimeServices(services);
            services.AddSingleton(_model);
            services.AddSingleton(relationalTypeMappingSource);

            using var serviceScope = services.BuildServiceProvider().CreateScope();
            _generator = serviceScope.ServiceProvider.GetRequiredService<IMigrationsCodeGenerator>();
        }

        [Test]
        public void GenerateMigration_CorrectCode()
        {
            //Arrange
            var standardOp1 = new RenameTableOperation { Name = "T_OLD", NewName = "T_NEW" };
            var sqlOp1 = new CreateOrUpdateSqlObjectOperation("v_view", "create v_view as select", 20);
            var sqlOp2 = new CreateOrUpdateSqlObjectOperation("v_view_2", "create v_view_2 as select", 30);
            var standardOp2 = new RenameTableOperation { Name = "T_OLD_2", NewName = "T_NEW_2" };
            var drop1 = new DropSqlObjectOperation(sqlOp1.Name, "DROP v_view", sqlOp1.Order);
            var drop2 = new DropSqlObjectOperation(sqlOp2.Name, "DROP v_view_2", sqlOp2.Order);

            var upOps = new MigrationOperation[]
            {
                standardOp1, sqlOp1, sqlOp2, standardOp2
            };

            var downOps = new MigrationOperation[]
            {
                standardOp1, drop1, drop2, standardOp2
            };

            //Act
            var migration = _generator.GenerateMigration("Custom.DataAccess", "TestMigration",
                upOps, downOps);

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