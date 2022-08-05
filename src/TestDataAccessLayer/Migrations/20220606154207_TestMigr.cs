using CUSTIS.NetCore.EF.MigrationGenerationExtensions.SqlObjects;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace TestDataAccessLayer.Migrations
{
    public partial class TestMigr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "migr_ext_tests");

            migrationBuilder.CreateTable(
                name: "my_table",
                schema: "migr_ext_tests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_my_table", x => x.Id);
                });

            migrationBuilder.Sql(
                sql: "create view name123_2 as select * from migr_ext_tests.my_table");

            migrationBuilder.Sql(
                sql: "create view name123 as select * from migr_ext_tests.my_table");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "my_table",
                schema: "migr_ext_tests");
        }
    }
}
