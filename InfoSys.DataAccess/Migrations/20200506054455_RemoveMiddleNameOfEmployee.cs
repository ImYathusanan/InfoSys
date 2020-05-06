using Microsoft.EntityFrameworkCore.Migrations;

namespace InfoSys.DataAccess.Migrations
{
    public partial class RemoveMiddleNameOfEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "Employees");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "Employees",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
