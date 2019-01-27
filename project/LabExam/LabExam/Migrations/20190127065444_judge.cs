using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LabExam.Migrations
{
    public partial class judge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExamJudgeChoices",
                columns: table => new
                {
                    ExamJudgeChoicesId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PaperId = table.Column<int>(nullable: false),
                    StudentId = table.Column<string>(maxLength: 40, nullable: true),
                    JudgeId = table.Column<int>(nullable: false),
                    StudentAnswer = table.Column<string>(maxLength: 10, nullable: true),
                    RealAnswer = table.Column<string>(nullable: true),
                    Score = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamJudgeChoices", x => x.ExamJudgeChoicesId);
                    table.ForeignKey(
                        name: "FK_ExamJudgeChoices_JudgeChoices_JudgeId",
                        column: x => x.JudgeId,
                        principalTable: "JudgeChoices",
                        principalColumn: "JudgeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamJudgeChoices_ExaminationPapers_PaperId",
                        column: x => x.PaperId,
                        principalTable: "ExaminationPapers",
                        principalColumn: "PaperId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamMultipleChoices",
                columns: table => new
                {
                    ExamMultipleChoicesId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PaperId = table.Column<int>(nullable: false),
                    StudentId = table.Column<string>(maxLength: 40, nullable: true),
                    MultipleId = table.Column<int>(nullable: false),
                    StudentAnswer = table.Column<string>(maxLength: 10, nullable: true),
                    RealAnswer = table.Column<string>(nullable: true),
                    Score = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamMultipleChoices", x => x.ExamMultipleChoicesId);
                    table.ForeignKey(
                        name: "FK_ExamMultipleChoices_MultipleChoices_MultipleId",
                        column: x => x.MultipleId,
                        principalTable: "MultipleChoices",
                        principalColumn: "MultipleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamMultipleChoices_ExaminationPapers_PaperId",
                        column: x => x.PaperId,
                        principalTable: "ExaminationPapers",
                        principalColumn: "PaperId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExamJudgeChoices_JudgeId",
                table: "ExamJudgeChoices",
                column: "JudgeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamJudgeChoices_PaperId",
                table: "ExamJudgeChoices",
                column: "PaperId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamMultipleChoices_MultipleId",
                table: "ExamMultipleChoices",
                column: "MultipleId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamMultipleChoices_PaperId",
                table: "ExamMultipleChoices",
                column: "PaperId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExamJudgeChoices");

            migrationBuilder.DropTable(
                name: "ExamMultipleChoices");
        }
    }
}
