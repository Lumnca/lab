using Microsoft.EntityFrameworkCore.Migrations;

namespace LabExam.Migrations
{
    public partial class reviewPaper : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.AlterColumn<string>(
                name: "StuOperationContent",
                table: "LogStudentOperations",
                type: "nvarchar(600)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Review",
                table: "ExaminationPapers",
                maxLength: 600,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Review",
                table: "ExaminationPapers");

            migrationBuilder.AlterColumn<string>(
                name: "StuOperationContent",
                table: "LogStudentOperations",
                type: "nvarchar(300)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(600)",
                oldNullable: true);
        }
    }
}
