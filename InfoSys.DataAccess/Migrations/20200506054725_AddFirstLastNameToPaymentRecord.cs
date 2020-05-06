using Microsoft.EntityFrameworkCore.Migrations;

namespace InfoSys.DataAccess.Migrations
{
    public partial class AddFirstLastNameToPaymentRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "PaymentRecords");

            migrationBuilder.AddColumn<string>(
                name: "Firstname",
                table: "PaymentRecords",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lastname",
                table: "PaymentRecords",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Firstname",
                table: "PaymentRecords");

            migrationBuilder.DropColumn(
                name: "Lastname",
                table: "PaymentRecords");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "PaymentRecords",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
