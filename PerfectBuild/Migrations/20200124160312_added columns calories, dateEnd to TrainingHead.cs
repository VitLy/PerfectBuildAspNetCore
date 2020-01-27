using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerfectBuild.Migrations
{
    public partial class addedcolumnscaloriesdateEndtoTrainingHead : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Calories",
                table: "TrainingHeads",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateEnd",
                table: "TrainingHeads",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Calories",
                table: "TrainingHeads");

            migrationBuilder.DropColumn(
                name: "DateEnd",
                table: "TrainingHeads");
        }
    }
}
