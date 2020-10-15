using Microsoft.EntityFrameworkCore.Migrations;

namespace PerfectBuild.Migrations
{
    public partial class AddedDeleteBehaviorRestrict : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Units_UnitId",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingPlanSpecs_Exercises_ExId",
                table: "TrainingPlanSpecs");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingProgramHeads_Categories_CategoryId",
                table: "TrainingProgramHeads");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingProgramSpecs_Exercises_ExId",
                table: "TrainingProgramSpecs");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingSpecs_Exercises_ExId",
                table: "TrainingSpecs");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingSpecs_TrainingHeads_HeadId",
                table: "TrainingSpecs");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Units_UnitId",
                table: "Exercises",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingPlanSpecs_Exercises_ExId",
                table: "TrainingPlanSpecs",
                column: "ExId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingProgramHeads_Categories_CategoryId",
                table: "TrainingProgramHeads",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingProgramSpecs_Exercises_ExId",
                table: "TrainingProgramSpecs",
                column: "ExId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingSpecs_Exercises_ExId",
                table: "TrainingSpecs",
                column: "ExId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingSpecs_TrainingHeads_HeadId",
                table: "TrainingSpecs",
                column: "HeadId",
                principalTable: "TrainingHeads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Units_UnitId",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingPlanSpecs_Exercises_ExId",
                table: "TrainingPlanSpecs");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingProgramHeads_Categories_CategoryId",
                table: "TrainingProgramHeads");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingProgramSpecs_Exercises_ExId",
                table: "TrainingProgramSpecs");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingSpecs_Exercises_ExId",
                table: "TrainingSpecs");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingSpecs_TrainingHeads_HeadId",
                table: "TrainingSpecs");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Units_UnitId",
                table: "Exercises",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingPlanSpecs_Exercises_ExId",
                table: "TrainingPlanSpecs",
                column: "ExId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingProgramHeads_Categories_CategoryId",
                table: "TrainingProgramHeads",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingProgramSpecs_Exercises_ExId",
                table: "TrainingProgramSpecs",
                column: "ExId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingSpecs_Exercises_ExId",
                table: "TrainingSpecs",
                column: "ExId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingSpecs_TrainingHeads_HeadId",
                table: "TrainingSpecs",
                column: "HeadId",
                principalTable: "TrainingHeads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
