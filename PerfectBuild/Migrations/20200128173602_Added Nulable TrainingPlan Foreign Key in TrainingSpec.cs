using Microsoft.EntityFrameworkCore.Migrations;

namespace PerfectBuild.Migrations
{
    public partial class AddedNulableTrainingPlanForeignKeyinTrainingSpec : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingSpecs_TrainingPlanHeads_TrainigPlanId",
                table: "TrainingSpecs");

            migrationBuilder.AlterColumn<int>(
                name: "TrainigPlanId",
                table: "TrainingSpecs",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "TrainingSpecs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingSpecs_TrainingPlanHeads_TrainigPlanId",
                table: "TrainingSpecs",
                column: "TrainigPlanId",
                principalTable: "TrainingPlanHeads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingSpecs_TrainingPlanHeads_TrainigPlanId",
                table: "TrainingSpecs");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "TrainingSpecs");

            migrationBuilder.AlterColumn<int>(
                name: "TrainigPlanId",
                table: "TrainingSpecs",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingSpecs_TrainingPlanHeads_TrainigPlanId",
                table: "TrainingSpecs",
                column: "TrainigPlanId",
                principalTable: "TrainingPlanHeads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
