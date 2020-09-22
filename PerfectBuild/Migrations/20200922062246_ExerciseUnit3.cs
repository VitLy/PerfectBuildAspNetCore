using Microsoft.EntityFrameworkCore.Migrations;

namespace PerfectBuild.Migrations
{
    public partial class ExerciseUnit3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Exercises_UnitId",
                table: "Exercises",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Units_UnitId",
                table: "Exercises",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Units_UnitId",
                table: "Exercises");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_UnitId",
                table: "Exercises");
        }
    }
}
