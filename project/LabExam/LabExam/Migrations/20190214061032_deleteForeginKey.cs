using Microsoft.EntityFrameworkCore.Migrations;

namespace LabExam.Migrations
{
    public partial class deleteForeginKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Module_Principal_PrincipalId",
                table: "Module");

            migrationBuilder.DropIndex(
                name: "IX_Module_PrincipalId",
                table: "Module");

            migrationBuilder.EnsureSchema(
                name: "dbo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Module_PrincipalId",
                table: "Module",
                column: "PrincipalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Module_Principal_PrincipalId",
                table: "Module",
                column: "PrincipalId",
                principalTable: "Principal",
                principalColumn: "PrincipalId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
