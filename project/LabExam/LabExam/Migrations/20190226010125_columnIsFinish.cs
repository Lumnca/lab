using Microsoft.EntityFrameworkCore.Migrations;

namespace LabExam.Migrations
{
    public partial class columnIsFinish : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.AddColumn<bool>(
                name: "IsFinish",
                table: "ExaminationPapers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFinish",
                table: "ExaminationPapers");
        }
    }
}
