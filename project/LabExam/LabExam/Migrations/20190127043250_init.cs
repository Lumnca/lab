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
                name: "ExaminationPapers",
                columns: table => new
                {
                    PaperId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StudentId = table.Column<string>(nullable: true),
                    PassScore = table.Column<float>(nullable: false),
                    ExamTime = table.Column<float>(nullable: false),
                    LeaveExamTime = table.Column<float>(nullable: false),
                    TotleScore = table.Column<float>(nullable: false),
                    AddTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationPapers", x => x.PaperId);
                });

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
                name: "Cources",
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
                    table.PrimaryKey("PK_Cources", x => x.CourceId);
                    table.ForeignKey(
                        name: "FK_Cources_Module_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Module",
                        principalColumn: "ModuleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cources_Principal_PrincipalId",
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
                name: "JudgeChoices",
                columns: table => new
                {
                    JudgeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModuleId = table.Column<int>(nullable: false),
                    Content = table.Column<string>(type: "ntext", nullable: true),
                    Answer = table.Column<string>(maxLength: 10, nullable: true),
                    DegreeOfDifficulty = table.Column<float>(nullable: false),
                    AddTime = table.Column<DateTime>(nullable: false),
                    PrincipalId = table.Column<string>(maxLength: 100, nullable: true),
                    Count = table.Column<int>(nullable: false),
                    A = table.Column<string>(maxLength: 1000, nullable: true),
                    B = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JudgeChoices", x => x.JudgeId);
                    table.ForeignKey(
                        name: "FK_JudgeChoices_Module_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Module",
                        principalColumn: "ModuleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MultipleChoices",
                columns: table => new
                {
                    MultipleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModuleId = table.Column<int>(nullable: false),
                    Content = table.Column<string>(type: "ntext", nullable: true),
                    Answer = table.Column<string>(maxLength: 10, nullable: true),
                    DegreeOfDifficulty = table.Column<float>(nullable: false),
                    AddTime = table.Column<DateTime>(nullable: false),
                    PrincipalId = table.Column<string>(maxLength: 100, nullable: true),
                    Count = table.Column<int>(nullable: false),
                    A = table.Column<string>(maxLength: 1000, nullable: true),
                    B = table.Column<string>(maxLength: 1000, nullable: true),
                    C = table.Column<string>(maxLength: 1000, nullable: true),
                    D = table.Column<string>(maxLength: 1000, nullable: true),
                    E = table.Column<string>(maxLength: 1000, nullable: true),
                    F = table.Column<string>(maxLength: 1000, nullable: true),
                    G = table.Column<string>(maxLength: 1000, nullable: true),
                    H = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultipleChoices", x => x.MultipleId);
                    table.ForeignKey(
                        name: "FK_MultipleChoices_Module_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Module",
                        principalColumn: "ModuleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SingleChoices",
                columns: table => new
                {
                    SingleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModuleId = table.Column<int>(nullable: false),
                    Content = table.Column<string>(type: "ntext", nullable: true),
                    Answer = table.Column<string>(maxLength: 10, nullable: true),
                    DegreeOfDifficulty = table.Column<float>(nullable: false),
                    AddTime = table.Column<DateTime>(nullable: false),
                    PrincipalId = table.Column<string>(maxLength: 100, nullable: true),
                    Count = table.Column<int>(nullable: false),
                    A = table.Column<string>(maxLength: 1000, nullable: true),
                    B = table.Column<string>(maxLength: 1000, nullable: true),
                    C = table.Column<string>(maxLength: 1000, nullable: true),
                    D = table.Column<string>(maxLength: 1000, nullable: true),
                    E = table.Column<string>(maxLength: 1000, nullable: true),
                    F = table.Column<string>(maxLength: 1000, nullable: true),
                    G = table.Column<string>(maxLength: 1000, nullable: true),
                    H = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SingleChoices", x => x.SingleId);
                    table.ForeignKey(
                        name: "FK_SingleChoices_Module_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Module",
                        principalColumn: "ModuleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    ResourceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    CourceId = table.Column<int>(nullable: false),
                    ResourceType = table.Column<int>(nullable: false),
                    Description = table.Column<string>(type: "ntext", nullable: true),
                    LengthOfStudy = table.Column<float>(nullable: false),
                    ResourceStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.ResourceId);
                    table.ForeignKey(
                        name: "FK_Resources_Cources_CourceId",
                        column: x => x.CourceId,
                        principalTable: "Cources",
                        principalColumn: "CourceId",
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

            migrationBuilder.CreateTable(
                name: "Learings",
                columns: table => new
                {
                    LearingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StudentId = table.Column<string>(maxLength: 40, nullable: true),
                    CourceId = table.Column<int>(nullable: false),
                    AddTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Learings", x => x.LearingId);
                    table.ForeignKey(
                        name: "FK_Learings_Cources_CourceId",
                        column: x => x.CourceId,
                        principalTable: "Cources",
                        principalColumn: "CourceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Learings_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Progresses",
                columns: table => new
                {
                    ProgressId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StudentId = table.Column<string>(maxLength: 40, nullable: true),
                    ResourceId = table.Column<int>(nullable: false),
                    StudyTime = table.Column<float>(nullable: false),
                    NeedTime = table.Column<float>(nullable: false),
                    AddTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Progresses", x => x.ProgressId);
                    table.ForeignKey(
                        name: "FK_Progresses_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "ResourceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Progresses_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cources_ModuleId",
                table: "Cources",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Cources_PrincipalId",
                table: "Cources",
                column: "PrincipalId");

            migrationBuilder.CreateIndex(
                name: "IX_Institute_ModuleId",
                table: "Institute",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_JudgeChoices_ModuleId",
                table: "JudgeChoices",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Learings_CourceId",
                table: "Learings",
                column: "CourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Learings_StudentId",
                table: "Learings",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Module_PrincipalId",
                table: "Module",
                column: "PrincipalId");

            migrationBuilder.CreateIndex(
                name: "IX_MultipleChoices_ModuleId",
                table: "MultipleChoices",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Professions_InstituteId",
                table: "Professions",
                column: "InstituteId");

            migrationBuilder.CreateIndex(
                name: "IX_Progresses_ResourceId",
                table: "Progresses",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Progresses_StudentId",
                table: "Progresses",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_CourceId",
                table: "Resources",
                column: "CourceId");

            migrationBuilder.CreateIndex(
                name: "IX_SingleChoices_ModuleId",
                table: "SingleChoices",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_ProfessionId",
                table: "Student",
                column: "ProfessionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExaminationPapers");

            migrationBuilder.DropTable(
                name: "JudgeChoices");

            migrationBuilder.DropTable(
                name: "Learings");

            migrationBuilder.DropTable(
                name: "MultipleChoices");

            migrationBuilder.DropTable(
                name: "Progresses");

            migrationBuilder.DropTable(
                name: "SingleChoices");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Cources");

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
