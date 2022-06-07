using System;
using CUSTIS.NetCore.EF.MigrationGenerationExtensions.Configuration;
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

            var options = builder.Options.AddDesignTimeServicesExtension();

            return new TestContext(options);
        }
    }
}