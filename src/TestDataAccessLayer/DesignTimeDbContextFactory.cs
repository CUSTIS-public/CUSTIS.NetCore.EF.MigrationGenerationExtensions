using System;
using CUSTIS.NetCore.EF.MigrationGenerationExtensions.Configuration;
using CUSTIS.NetCore.EF.MigrationGenerationExtensions.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TestDataAccessLayer
{
    internal class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TestContext>
    {
        public TestContext CreateDbContext(string[] args)
        {
            Console.WriteLine($"Using DesignTimeDbContextFactory");

            var builder = new DbContextOptionsBuilder<TestContext>();

            builder.UseSqlObjects();

            return new TestContext(builder.Options);
        }
    }
}