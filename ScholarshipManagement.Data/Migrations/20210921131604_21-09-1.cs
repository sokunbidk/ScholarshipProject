using Microsoft.EntityFrameworkCore.Migrations;

namespace ScholarshipManagement.Data.Migrations
{
    public partial class _21091 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BankAccountName",
                table: "Payments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankAccountNumber",
                table: "Payments",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BankName",
                table: "Payments",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankAccountName",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "BankAccountNumber",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "BankName",
                table: "Payments");
        }
    }
}
