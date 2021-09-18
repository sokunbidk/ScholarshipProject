using Microsoft.EntityFrameworkCore.Migrations;

namespace ScholarshipManagement.Data.Migrations
{
    public partial class _17094 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PresidentId",
                table: "Circuits");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PresidentId",
                table: "Circuits",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
