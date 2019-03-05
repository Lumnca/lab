using Microsoft.EntityFrameworkCore.Migrations;

namespace LabExam.Migrations
{
    public partial class updateIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateIndex(
                name: "IX_Learings_CourceId",
                table: "Learings",
                column: "CourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Learings_StudentId",
                table: "Learings",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Learings_CourceId",
                table: "Learings");

            migrationBuilder.DropIndex(
                name: "IX_Learings_StudentId",
                table: "Learings");
        }
    }
}
