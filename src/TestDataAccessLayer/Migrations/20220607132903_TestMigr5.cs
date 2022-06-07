using CUSTIS.NetCore.EF.MigrationGenerationExtensions.SqlObjects;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDataAccessLayer.Migrations
{
    public partial class TestMigr5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                sql: "create or replace view my_view_2 as\r\nselect * from t_table\r\n;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
