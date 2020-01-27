using Microsoft.EntityFrameworkCore.Migrations;

namespace PerfectBuild.Migrations
{
    public partial class ChangedfieldstoHeadIdinSpec : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingProgramSpecs_TrainingProgramHeads_ProgramHeadId",
                table: "TrainingProgramSpecs");

            migrationBuilder.RenameColumn(
                name: "ProgramHeadId",
                table: "TrainingPlanSpecs",
                newName: "HeadId");

            migrationBuilder.AlterColumn<int>(
                name: "ProgramHeadId",
                table: "TrainingProgramSpecs",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "HeadId",
                table: "TrainingProgramSpecs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingProgramSpecs_TrainingProgramHeads_ProgramHeadId",
                table: "TrainingProgramSpecs",
                column: "ProgramHeadId",
                principalTable: "TrainingProgramHeads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingProgramSpecs_TrainingProgramHeads_ProgramHeadId",
                table: "TrainingProgramSpecs");

            migrationBuilder.DropColumn(
                name: "HeadId",
                table: "TrainingProgramSpecs");

            migrationBuilder.RenameColumn(
                name: "HeadId",
                table: "TrainingPlanSpecs",
                newName: "ProgramHeadId");

            migrationBuilder.AlterColumn<int>(
                name: "ProgramHeadId",
                table: "TrainingProgramSpecs",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingProgramSpecs_TrainingProgramHeads_ProgramHeadId",
                table: "TrainingProgramSpecs",
                column: "ProgramHeadId",
                principalTable: "TrainingProgramHeads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
