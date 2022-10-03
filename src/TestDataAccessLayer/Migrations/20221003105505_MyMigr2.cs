using CUSTIS.NetCore.EF.MigrationGenerationExtensions.SqlObjects;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDataAccessLayer.Migrations
{
    public partial class MyMigr2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateOrUpdateSqlObject(
                name: "v_view_10",
                sqlCode: "create or replace view migr_ext_tests.v_view_10 as select * from migr_ext_tests.my_table;",
                order: 10);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSqlObject(
                name: "v_view_10",
                sqlCode: "DROP v_view_10", // write code to drop object
                order: 10);

            migrationBuilder.CreateOrUpdateSqlObject(
                name: "my_stored_proc",
                sqlCode: "stored_proc_code",
                order: 10);
        }
    }
}
