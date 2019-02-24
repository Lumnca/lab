using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LabExam.Migrations
{
    public partial class createAddTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.AddColumn<DateTime>(
                name: "AddTime",
                table: "ApplicationForReExaminations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddTime",
                table: "ApplicationForReExaminations");
        }
    }
}
