using Microsoft.EntityFrameworkCore.Migrations;

namespace LabExam.Migrations
{
    public partial class deleteForegin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Learings_Cources_CourceId",
                table: "Learings");

            migrationBuilder.DropForeignKey(
                name: "FK_Learings_Student_StudentId",
                table: "Learings");

            migrationBuilder.DropIndex(
                name: "IX_Learings_CourceId",
                table: "Learings");

            migrationBuilder.DropIndex(
                name: "IX_Learings_StudentId",
                table: "Learings");

            migrationBuilder.EnsureSchema(
                name: "dbo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Learings_CourceId",
                table: "Learings",
                column: "CourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Learings_StudentId",
                table: "Learings",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Learings_Cources_CourceId",
                table: "Learings",
                column: "CourceId",
                principalTable: "Cources",
                principalColumn: "CourceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Learings_Student_StudentId",
                table: "Learings",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
