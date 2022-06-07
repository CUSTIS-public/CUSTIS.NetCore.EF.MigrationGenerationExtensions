using System.Linq;
using CUSTIS.NetCore.EF.MigrationGenerationExtensions.ObjectSources;
using NUnit.Framework;
using TestDataAccessLayer;

namespace UnitTests.ObjectSources
{
    public class AssemblyExtensionsTests
    {
        [Test]
        public void GetAllSql_CorrectResult()
        {
            //Arrange

            //Act
            var resources = typeof(Class1).Assembly.GetAllSql("Sql");

            //Assert
            Assert.That(resources.Select(r => r.ResourceName), Contains.Item("v_view.sql"));
            Assert.That(resources.FirstOrDefault(r => r.ResourceName == "v_view.sql")?.FullName, Is.EqualTo("TestDataAccessLayer.Sql.v_view.sql"));
        }
    }
}