using Microsoft.EntityFrameworkCore.Migrations;

namespace InfoSys.DataAccess.Migrations
{
    public partial class AddingPhoneToEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Phone",
                table: "Employees",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Employees");
        }
    }
}
