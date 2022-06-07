﻿// <auto-generated />
using CUSTIS.NetCore.EF.MigrationGenerationExtensions.SqlObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TestDataAccessLayer;

namespace TestDataAccessLayer.Migrations
{
    [DbContext(typeof(TestContext))]
    partial class TestContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.Model.AddSqlObjects(new SqlObject[]
            {
                new("test2", "create view name123_2 as select * from t_t")
                {
                    Order = 10
                },
                new("test1", "create view name123 as select * from t_t")
                {
                    Order = 2147483647
                },
                new("v_view_2.sql", "create or replace view my_view_2 as\r\nselect * from t_table\r\n;")
                {
                    Order = 2147483647
                },
                new("v_view.sql", "create or replace view my_view as\r\nselect * from t_table\r\n;")
                {
                    Order = 2147483647
                },
            });

            modelBuilder
                .HasDefaultSchema("migr_ext_tests")
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("TestDataAccessLayer.Class1", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.HasKey("Id");

                    b.ToTable("my_table");
                });
#pragma warning restore 612, 618
        }
    }
}
