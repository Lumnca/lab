using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LabExam.Migrations
{
    public partial class finish : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationForReExaminations",
                columns: table => new
                {
                    ApplicationExamId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StudentId = table.Column<string>(maxLength: 40, nullable: true),
                    ModuleId = table.Column<int>(nullable: false),
                    InstituteId = table.Column<int>(nullable: false),
                    Reason = table.Column<string>(type: "ntext", nullable: true),
                    Email = table.Column<string>(maxLength: 500, nullable: true),
                    ApplicationStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationForReExaminations", x => x.ApplicationExamId);
                    table.ForeignKey(
                        name: "FK_ApplicationForReExaminations_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationJoinTheExaminations",
                columns: table => new
                {
                    ApplicationJoinId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StudentId = table.Column<string>(maxLength: 40, nullable: true),
                    Reason = table.Column<string>(type: "ntext", nullable: true),
                    Email = table.Column<string>(maxLength: 300, nullable: true),
                    IDNumber = table.Column<string>(maxLength: 300, nullable: true),
                    InstituteId = table.Column<int>(nullable: false),
                    ProfessionId = table.Column<int>(nullable: false),
                    Grade = table.Column<int>(nullable: false),
                    Phone = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(maxLength: 80, nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    Sex = table.Column<bool>(nullable: false),
                    StudentType = table.Column<int>(nullable: false),
                    ApplicationStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationJoinTheExaminations", x => x.ApplicationJoinId);
                });

            migrationBuilder.CreateTable(
                name: "LogPricipalOperations",
                columns: table => new
                {
                    LogPricipalOperationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PrincipalId = table.Column<string>(maxLength: 100, nullable: true),
                    OperationIp = table.Column<string>(maxLength: 50, nullable: true),
                    AddTime = table.Column<DateTime>(nullable: false),
                    PrincpalOperationCode = table.Column<int>(nullable: false),
                    PrincpalOperationName = table.Column<string>(maxLength: 40, nullable: true),
                    PrincpalOperationContent = table.Column<string>(maxLength: 300, nullable: true),
                    PrincpalOperationStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogPricipalOperations", x => x.LogPricipalOperationId);
                });

            migrationBuilder.CreateTable(
                name: "LogStudentOperations",
                columns: table => new
                {
                    LogStudentOperationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StudentId = table.Column<int>(maxLength: 80, nullable: false),
                    StuOperationCode = table.Column<int>(nullable: false),
                    StuOperationStatus = table.Column<int>(nullable: false),
                    StuOperationName = table.Column<string>(maxLength: 40, nullable: true),
                    StuOperationType = table.Column<int>(nullable: false),
                    AddTime = table.Column<DateTime>(nullable: false),
                    StuOperationContent = table.Column<string>(type: "nvarchar(300)", nullable: true),
                    OperationIp = table.Column<string>(maxLength: 50, nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogStudentOperations", x => x.LogStudentOperationId);
                });

            migrationBuilder.CreateTable(
                name: "LogUserLogin",
                columns: table => new
                {
                    LogUserLoginId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID = table.Column<string>(maxLength: 60, nullable: true),
                    LoginTime = table.Column<DateTime>(nullable: false),
                    LoginDate = table.Column<DateTime>(nullable: false),
                    LoginIp = table.Column<string>(maxLength: 100, nullable: true),
                    Terminal = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogUserLogin", x => x.LogUserLoginId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationForReExaminations_StudentId",
                table: "ApplicationForReExaminations",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationForReExaminations");

            migrationBuilder.DropTable(
                name: "ApplicationJoinTheExaminations");

            migrationBuilder.DropTable(
                name: "LogPricipalOperations");

            migrationBuilder.DropTable(
                name: "LogStudentOperations");

            migrationBuilder.DropTable(
                name: "LogUserLogin");
        }
    }
}
