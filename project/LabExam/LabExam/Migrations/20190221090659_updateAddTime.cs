using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LabExam.Migrations
{
    public partial class updateAddTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.AddColumn<DateTime>(
                name: "AddTime",
                table: "ApplicationJoinTheExaminations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddTime",
                table: "ApplicationJoinTheExaminations");
        }
    }
}
