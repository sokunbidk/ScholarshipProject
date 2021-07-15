using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace ScholarshipManagement.Data.Migrations
{
    public partial class firstmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationForms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    SchoolSession = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Level = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    NameOfSchool = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    InstitutionType = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Discipline = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Duration = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    DegreeInView = table.Column<string>(type: "text", nullable: true),
                    LikeyCompletionYear = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    LetterOfAdmission = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    SchoolBill = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    AcademenicLevel = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    AmountRequested = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    BankAccount = table.Column<int>(type: "int", maxLength: 10, nullable: false),
                    BankName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    LastSchoolResult = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    MemberCode = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationForms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FirstBankOfNigeriaPlc = table.Column<string>(type: "text", nullable: true),
                    UnionBankOfNigeriaPlc = table.Column<string>(type: "text", nullable: true),
                    ZenithBankPlc = table.Column<string>(type: "text", nullable: true),
                    UnitedBankPlc = table.Column<string>(type: "text", nullable: true),
                    FirstCityMonumentBankPlc = table.Column<string>(type: "text", nullable: true),
                    StabicIBTCBankPlc = table.Column<string>(type: "text", nullable: true),
                    WemaBankPlc = table.Column<string>(type: "text", nullable: true),
                    StarlingBankPlc = table.Column<string>(type: "text", nullable: true),
                    GuaranteeTrustBankPlc = table.Column<string>(type: "text", nullable: true),
                    UnityBankPlc = table.Column<string>(type: "text", nullable: true),
                    PolarisBankPlc = table.Column<string>(type: "text", nullable: true),
                    StandardCharteredBankPlc = table.Column<string>(type: "text", nullable: true),
                    MemberCode = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    AmountRequested = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    AmountRecommended = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    AmountApprovedAndGranted = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    DateApproved = table.Column<DateTime>(type: "datetime", nullable: false),
                    ConfirmPayment = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DatePaid = table.Column<DateTime>(type: "datetime", nullable: false),
                    BankAccount = table.Column<int>(type: "int", nullable: false),
                    BankName = table.Column<string>(type: "text", nullable: true),
                    ApprovedBy = table.Column<string>(type: "text", nullable: true),
                    ProofOfChandaPmt = table.Column<string>(type: "text", nullable: true),
                    MemberCode = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    RoleName = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    MemberCode = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    SurName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    OtherName = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    Address = table.Column<decimal>(type: "decimal(18, 2)", maxLength: 50, nullable: false),
                    Jamaat = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    Circuit = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    PhoneNO = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    Email = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Gender = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime", maxLength: 10, nullable: false),
                    AuxiliaryBody = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    Guidian = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    GuidianPhoneNo = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false),
                    Photograph = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    MemberCode = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    PhoneNo = table.Column<string>(type: "text", nullable: true),
                    MemberCode = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationForms");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
