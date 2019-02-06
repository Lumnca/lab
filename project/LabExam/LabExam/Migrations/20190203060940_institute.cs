using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LabExam.Migrations
{
    public partial class institute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Institute_Module_ModuleId",
                table: "Institute");

            migrationBuilder.DropIndex(
                name: "IX_Institute_ModuleId",
                table: "Institute");

            migrationBuilder.DropColumn(
                name: "ModuleId",
                table: "Institute");

            migrationBuilder.CreateTable(
                name: "InstituteToModules",
                columns: table => new
                {
                    InstituteToModuleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModuleId = table.Column<int>(nullable: false),
                    InstituteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstituteToModules", x => x.InstituteToModuleId);
                    table.ForeignKey(
                        name: "FK_InstituteToModules_Institute_InstituteId",
                        column: x => x.InstituteId,
                        principalTable: "Institute",
                        principalColumn: "InstituteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstituteToModules_Module_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Module",
                        principalColumn: "ModuleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InstituteToModules_InstituteId",
                table: "InstituteToModules",
                column: "InstituteId");

            migrationBuilder.CreateIndex(
                name: "IX_InstituteToModules_ModuleId",
                table: "InstituteToModules",
                column: "ModuleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstituteToModules");

            migrationBuilder.AddColumn<int>(
                name: "ModuleId",
                table: "Institute",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Institute_ModuleId",
                table: "Institute",
                column: "ModuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Institute_Module_ModuleId",
                table: "Institute",
                column: "ModuleId",
                principalTable: "Module",
                principalColumn: "ModuleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
