using Microsoft.EntityFrameworkCore.Migrations;

namespace PerfectBuild.Migrations
{
    public partial class DeleteTrainingPlanForeignKeyinTrainingSpec : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingSpecs_TrainingPlanHeads_TrainigPlanId",
                table: "TrainingSpecs");

            migrationBuilder.DropIndex(
                name: "IX_TrainingSpecs_TrainigPlanId",
                table: "TrainingSpecs");

            migrationBuilder.DropColumn(
                name: "TrainigPlanId",
                table: "TrainingSpecs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrainigPlanId",
                table: "TrainingSpecs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingSpecs_TrainigPlanId",
                table: "TrainingSpecs",
                column: "TrainigPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingSpecs_TrainingPlanHeads_TrainigPlanId",
                table: "TrainingSpecs",
                column: "TrainigPlanId",
                principalTable: "TrainingPlanHeads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
