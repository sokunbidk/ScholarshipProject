using Microsoft.EntityFrameworkCore.Migrations;

namespace ScholarshipManagement.Data.Migrations
{
    public partial class _20091 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Applications_ApplicationFormId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_ApplicationFormId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "ApplicationFormId",
                table: "Payments");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ApplicationId",
                table: "Payments",
                column: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Applications_ApplicationId",
                table: "Payments",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Applications_ApplicationId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_ApplicationId",
                table: "Payments");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationFormId",
                table: "Payments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ApplicationFormId",
                table: "Payments",
                column: "ApplicationFormId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Applications_ApplicationFormId",
                table: "Payments",
                column: "ApplicationFormId",
                principalTable: "Applications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
