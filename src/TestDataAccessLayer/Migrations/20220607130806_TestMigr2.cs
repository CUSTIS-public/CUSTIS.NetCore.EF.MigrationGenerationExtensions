using CUSTIS.NetCore.EF.MigrationGenerationExtensions.SqlObjects;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDataAccessLayer.Migrations
{
    public partial class TestMigr2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                sql: "create or replace view my_view as\r\nselect * from t_table;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
