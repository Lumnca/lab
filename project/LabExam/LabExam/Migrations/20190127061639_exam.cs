using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LabExam.Migrations
{
    public partial class exam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "ExaminationPapers",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ExamSingleChoices",
                columns: table => new
                {
                    ExamSingleChoicesId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PaperId = table.Column<int>(nullable: false),
                    StudentId = table.Column<string>(maxLength: 40, nullable: true),
                    SingleId = table.Column<int>(nullable: false),
                    StudentAnswer = table.Column<string>(maxLength: 10, nullable: true),
                    RealAnswer = table.Column<string>(nullable: true),
                    Score = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamSingleChoices", x => x.ExamSingleChoicesId);
                    table.ForeignKey(
                        name: "FK_ExamSingleChoices_ExaminationPapers_PaperId",
                        column: x => x.PaperId,
                        principalTable: "ExaminationPapers",
                        principalColumn: "PaperId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamSingleChoices_SingleChoices_SingleId",
                        column: x => x.SingleId,
                        principalTable: "SingleChoices",
                        principalColumn: "SingleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExamSingleChoices_PaperId",
                table: "ExamSingleChoices",
                column: "PaperId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamSingleChoices_SingleId",
                table: "ExamSingleChoices",
                column: "SingleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExamSingleChoices");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "ExaminationPapers",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 40,
                oldNullable: true);
        }
    }
}
