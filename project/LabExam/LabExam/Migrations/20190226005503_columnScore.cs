using Microsoft.EntityFrameworkCore.Migrations;

namespace LabExam.Migrations
{
    public partial class columnScore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.AddColumn<float>(
                name: "Score",
                table: "ExaminationPapers",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "ExaminationPapers");
        }
    }
}
