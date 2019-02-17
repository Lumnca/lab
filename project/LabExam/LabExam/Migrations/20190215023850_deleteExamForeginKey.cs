using Microsoft.EntityFrameworkCore.Migrations;

namespace LabExam.Migrations
{
    public partial class deleteExamForeginKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamJudgeChoices_JudgeChoices_JudgeId",
                table: "ExamJudgeChoices");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamMultipleChoices_MultipleChoices_MultipleId",
                table: "ExamMultipleChoices");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamSingleChoices_SingleChoices_SingleId",
                table: "ExamSingleChoices");

            migrationBuilder.DropIndex(
                name: "IX_ExamSingleChoices_SingleId",
                table: "ExamSingleChoices");

            migrationBuilder.DropIndex(
                name: "IX_ExamMultipleChoices_MultipleId",
                table: "ExamMultipleChoices");

            migrationBuilder.DropIndex(
                name: "IX_ExamJudgeChoices_JudgeId",
                table: "ExamJudgeChoices");

            migrationBuilder.EnsureSchema(
                name: "dbo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ExamSingleChoices_SingleId",
                table: "ExamSingleChoices",
                column: "SingleId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamMultipleChoices_MultipleId",
                table: "ExamMultipleChoices",
                column: "MultipleId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamJudgeChoices_JudgeId",
                table: "ExamJudgeChoices",
                column: "JudgeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamJudgeChoices_JudgeChoices_JudgeId",
                table: "ExamJudgeChoices",
                column: "JudgeId",
                principalTable: "JudgeChoices",
                principalColumn: "JudgeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamMultipleChoices_MultipleChoices_MultipleId",
                table: "ExamMultipleChoices",
                column: "MultipleId",
                principalTable: "MultipleChoices",
                principalColumn: "MultipleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamSingleChoices_SingleChoices_SingleId",
                table: "ExamSingleChoices",
                column: "SingleId",
                principalTable: "SingleChoices",
                principalColumn: "SingleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
