using Microsoft.EntityFrameworkCore.Migrations;

namespace ScholarshipManagement.Data.Migrations
{
    public partial class _2ndMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UderId",
                table: "Applications",
                newName: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Applications",
                newName: "UderId");
        }
    }
}
