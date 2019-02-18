using Microsoft.EntityFrameworkCore.Migrations;

namespace LabExam.Migrations
{
    public partial class lonk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.AddColumn<string>(
                name: "ResourceUrl",
                table: "Resources",
                maxLength: 600,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResourceUrl",
                table: "Resources");
        }
    }
}
