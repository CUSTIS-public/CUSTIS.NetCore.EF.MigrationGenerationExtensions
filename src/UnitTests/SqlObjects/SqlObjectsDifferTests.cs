using System;
using System.Collections.Generic;
using System.Linq;
using CUSTIS.NetCore.EF.MigrationGenerationExtensions.SqlObjects;
using Microsoft.EntityFrameworkCore.Metadata;
using Moq;
using NUnit.Framework;

namespace UnitTests.SqlObjects
{
    public class SqlObjectsDifferTests
    {
        private readonly SqlObjectsDiffer _differ = new();

        [Test]
        public void GetDiff_NoDifference_EmptyResult()
        {
            //Arrange
            var source = new SqlObject("v_view", "select * from t_t");
            var target = new SqlObject("v_view", "select * from t_t");

            //Act
            var diff = _differ.GetDiff(ToModel(source), ToModel(target));

            //Assert
            Assert.That(diff, Has.Count.EqualTo(0));
        }

        [Test]
        public void GetDiff_SqlCodeChanged_MigrationOperationCreated()
        {
            //Arrange
            var source = new SqlObject("v_view", "select * from t_t");
            var target = new SqlObject("v_view", "select * from t_new");

            //Act
            var diff = _differ.GetDiff(ToModel(source), ToModel(target));

            //Assert
            Assert.That(diff, Has.Count.EqualTo(1));
            Assert.That((diff.ElementAtOrDefault(0) as ExecuteSqlOperation)?.SqlCode, Is.EqualTo(target.SqlCode));
        }

        [Test]
        public void GetDiff_NewObject_MigrationOperationCreated()
        {
            //Arrange
            var source = Array.Empty<SqlObject>();
            var target = new SqlObject("v_view", "select * from t_new");

            //Act
            var diff = _differ.GetDiff(ToModel(source), ToModel(target));

            //Assert
            Assert.That(diff, Has.Count.EqualTo(1));
            Assert.That((diff.ElementAtOrDefault(0) as ExecuteSqlOperation)?.SqlCode, Is.EqualTo(target.SqlCode));
        }

        private static IRelationalModel ToModel(params SqlObject[] objects)
        {
            return Mock.Of<IRelationalModel>(m => m.Model[SqlObjectsModelExtensions.SqlObjectsData] == new List<SqlObject>(objects));
        }
    }
}