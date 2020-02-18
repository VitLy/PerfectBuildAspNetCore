using Microsoft.EntityFrameworkCore.Migrations;

namespace PerfectBuild.Migrations
{
    public partial class CheckTrainingHead : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingHeads_TrainingPlanHeads_TrainingPlanHeadId",
                table: "TrainingHeads");

            migrationBuilder.DropIndex(
                name: "IX_TrainingHeads_TrainingPlanHeadId",
                table: "TrainingHeads");

            migrationBuilder.DropColumn(
                name: "TrainigPlanHeadId",
                table: "TrainingHeads");

            migrationBuilder.AlterColumn<int>(
                name: "TrainingPlanHeadId",
                table: "TrainingHeads",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TrainingPlanHeadId",
                table: "TrainingHeads",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "TrainigPlanHeadId",
                table: "TrainingHeads",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingHeads_TrainingPlanHeadId",
                table: "TrainingHeads",
                column: "TrainingPlanHeadId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingHeads_TrainingPlanHeads_TrainingPlanHeadId",
                table: "TrainingHeads",
                column: "TrainingPlanHeadId",
                principalTable: "TrainingPlanHeads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
