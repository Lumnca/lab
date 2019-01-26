using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LabExam.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Principal",
                columns: table => new
                {
                    PrincipalId = table.Column<string>(maxLength: 100, nullable: false),
                    Password = table.Column<string>(maxLength: 600, nullable: true),
                    JobNumber = table.Column<string>(maxLength: 50, nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Phone = table.Column<string>(maxLength: 100, nullable: true),
                    PrincipalStatus = table.Column<int>(nullable: false),
                    PrincipalConfig = table.Column<string>(maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Principal", x => x.PrincipalId);
                });

            migrationBuilder.CreateTable(
                name: "Module",
                columns: table => new
                {
                    ModuleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    AddTime = table.Column<DateTime>(nullable: false),
                    PrincipalId = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Module", x => x.ModuleId);
                    table.ForeignKey(
                        name: "FK_Module_Principal_PrincipalId",
                        column: x => x.PrincipalId,
                        principalTable: "Principal",
                        principalColumn: "PrincipalId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cource",
                columns: table => new
                {
                    CourceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 300, nullable: true),
                    PrincipalId = table.Column<string>(maxLength: 100, nullable: true),
                    AddTime = table.Column<DateTime>(nullable: false),
                    Introduction = table.Column<string>(nullable: true),
                    Credit = table.Column<float>(nullable: false),
                    ModuleId = table.Column<int>(nullable: false),
                    CourceStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cource", x => x.CourceId);
                    table.ForeignKey(
                        name: "FK_Cource_Module_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Module",
                        principalColumn: "ModuleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cource_Principal_PrincipalId",
                        column: x => x.PrincipalId,
                        principalTable: "Principal",
                        principalColumn: "PrincipalId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Institute",
                columns: table => new
                {
                    InstituteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 80, nullable: true),
                    ModuleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Institute", x => x.InstituteId);
                    table.ForeignKey(
                        name: "FK_Institute_Module_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Module",
                        principalColumn: "ModuleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Professions",
                columns: table => new
                {
                    ProfessionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProfessionType = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 80, nullable: true),
                    InstituteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professions", x => x.ProfessionId);
                    table.ForeignKey(
                        name: "FK_Professions_Institute_InstituteId",
                        column: x => x.InstituteId,
                        principalTable: "Institute",
                        principalColumn: "InstituteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    StudentId = table.Column<string>(maxLength: 40, nullable: false),
                    Password = table.Column<string>(maxLength: 440, nullable: true),
                    IDNumber = table.Column<string>(maxLength: 800, nullable: true),
                    InstituteId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 80, nullable: true),
                    Grade = table.Column<int>(nullable: false),
                    Phone = table.Column<string>(maxLength: 200, nullable: true),
                    ProfessionId = table.Column<int>(nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Sex = table.Column<bool>(nullable: false),
                    StudentType = table.Column<int>(nullable: false),
                    Email = table.Column<string>(maxLength: 300, nullable: true),
                    IsPassExam = table.Column<bool>(nullable: false),
                    MaxExamScore = table.Column<float>(nullable: false),
                    MaxExamCount = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_Student_Professions_ProfessionId",
                        column: x => x.ProfessionId,
                        principalTable: "Professions",
                        principalColumn: "ProfessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cource_ModuleId",
                table: "Cource",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Cource_PrincipalId",
                table: "Cource",
                column: "PrincipalId");

            migrationBuilder.CreateIndex(
                name: "IX_Institute_ModuleId",
                table: "Institute",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Module_PrincipalId",
                table: "Module",
                column: "PrincipalId");

            migrationBuilder.CreateIndex(
                name: "IX_Professions_InstituteId",
                table: "Professions",
                column: "InstituteId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_ProfessionId",
                table: "Student",
                column: "ProfessionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cource");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Professions");

            migrationBuilder.DropTable(
                name: "Institute");

            migrationBuilder.DropTable(
                name: "Module");

            migrationBuilder.DropTable(
                name: "Principal");
        }
    }
}
