using Microsoft.EntityFrameworkCore.Migrations;

namespace LabExam.Migrations
{
    public partial class deleteprincipal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cources_Principal_PrincipalId",
                table: "Cources");

            migrationBuilder.DropIndex(
                name: "IX_Cources_PrincipalId",
                table: "Cources");

            migrationBuilder.DropColumn(
                name: "PrincipalId",
                table: "Cources");

            migrationBuilder.EnsureSchema(
                name: "dbo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PrincipalId",
                table: "Cources",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cources_PrincipalId",
                table: "Cources",
                column: "PrincipalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cources_Principal_PrincipalId",
                table: "Cources",
                column: "PrincipalId",
                principalTable: "Principal",
                principalColumn: "PrincipalId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
