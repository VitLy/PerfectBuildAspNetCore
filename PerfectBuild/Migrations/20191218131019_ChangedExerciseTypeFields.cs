using Microsoft.EntityFrameworkCore.Migrations;

namespace PerfectBuild.Migrations
{
    public partial class ChangedExerciseTypeFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TrainingProgramHeads_CategoryId",
                table: "TrainingProgramHeads",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPlanHeads_CategoryId",
                table: "TrainingPlanHeads",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingPlanHeads_Categories_CategoryId",
                table: "TrainingPlanHeads",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingProgramHeads_Categories_CategoryId",
                table: "TrainingProgramHeads",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingPlanHeads_Categories_CategoryId",
                table: "TrainingPlanHeads");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingProgramHeads_Categories_CategoryId",
                table: "TrainingProgramHeads");

            migrationBuilder.DropIndex(
                name: "IX_TrainingProgramHeads_CategoryId",
                table: "TrainingProgramHeads");

            migrationBuilder.DropIndex(
                name: "IX_TrainingPlanHeads_CategoryId",
                table: "TrainingPlanHeads");
        }
    }
}
