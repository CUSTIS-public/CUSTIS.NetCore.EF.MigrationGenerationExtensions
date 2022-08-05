using CUSTIS.NetCore.EF.MigrationGenerationExtensions.SqlObjects;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDataAccessLayer.Migrations
{
    public partial class MyMigr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSqlObject(
                name: "test1",
                sqlCode: "DROP VIEW name123_2", // write code to drop object
                order: 2147483647);

            migrationBuilder.DropSqlObject(
                name: "test1",
                sqlCode: "DROP VIEW name123", // write code to drop object
                order: 2147483647);

            migrationBuilder.CreateOrUpdateSqlObject(
                name: "v_view_2.sql",
                sqlCode: "create or replace view migr_ext_tests.name123_2 as select * from migr_ext_tests.my_table;",
                order: 2147483647);

            migrationBuilder.CreateOrUpdateSqlObject(
                name: "v_view.sql",
                sqlCode: "create or replace view migr_ext_tests.name123 as select * from migr_ext_tests.my_table;",
                order: 2147483647);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateOrUpdateSqlObject(
                name: "test2",
                sqlCode: "create view name123_2 as select * from t_t",
                order: 10);

            migrationBuilder.CreateOrUpdateSqlObject(
                name: "test1",
                sqlCode: "create view name123 as select * from t_t",
                order: 2147483647);

            migrationBuilder.CreateOrUpdateSqlObject(
                name: "v_view_2.sql",
                sqlCode: "create or replace view my_view_2 as\r\nselect * from t_table\r\n;",
                order: 2147483647);

            migrationBuilder.CreateOrUpdateSqlObject(
                name: "v_view.sql",
                sqlCode: "create or replace view my_view as\r\nselect * from t_table\r\n;",
                order: 2147483647);
        }
    }
}
