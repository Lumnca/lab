using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LabExam.Migrations
{
    public partial class addpricipalKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.AddColumn<DateTime>(
                name: "AddTime",
                table: "Resources",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PrincipalId",
                table: "Resources",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrincipalId",
                table: "Cources",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddTime",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "PrincipalId",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "PrincipalId",
                table: "Cources");
        }
    }
}
