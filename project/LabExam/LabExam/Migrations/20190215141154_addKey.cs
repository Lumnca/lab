using Microsoft.EntityFrameworkCore.Migrations;

namespace LabExam.Migrations
{
    public partial class addKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "SingleChoices",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "MultipleChoices",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "JudgeChoices",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Key",
                table: "SingleChoices");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "MultipleChoices");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "JudgeChoices");
        }
    }
}
