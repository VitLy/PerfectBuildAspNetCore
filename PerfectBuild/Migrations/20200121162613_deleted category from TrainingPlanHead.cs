using Microsoft.EntityFrameworkCore.Migrations;

namespace PerfectBuild.Migrations
{
    public partial class deletedcategoryfromTrainingPlanHead : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingPlanHeads_Categories_CategoryId",
                table: "TrainingPlanHeads");

            migrationBuilder.DropIndex(
                name: "IX_TrainingPlanHeads_CategoryId",
                table: "TrainingPlanHeads");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "TrainingPlanHeads");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "TrainingPlanHeads",
                nullable: false,
                defaultValue: 0);

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
        }
    }
}
