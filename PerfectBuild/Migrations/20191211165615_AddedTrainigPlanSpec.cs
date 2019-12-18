using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerfectBuild.Migrations
{
    public partial class AddedTrainigPlanSpec : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingSpec_Exercises_ExId",
                table: "TrainingSpec");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingSpec_TrainingHeads_HeadId",
                table: "TrainingSpec");

            migrationBuilder.DropTable(
                name: "Sets");

            migrationBuilder.DropTable(
                name: "TrainingSchedulers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainingSpec",
                table: "TrainingSpec");

            migrationBuilder.DropColumn(
                name: "AmountPlan",
                table: "TrainingSpec");

            migrationBuilder.RenameTable(
                name: "TrainingSpec",
                newName: "TrainingSpecs");

            migrationBuilder.RenameColumn(
                name: "UserProfileId",
                table: "TrainingHeads",
                newName: "TrainigPlanHeadId");

            migrationBuilder.RenameIndex(
                name: "IX_TrainingSpec_HeadId",
                table: "TrainingSpecs",
                newName: "IX_TrainingSpecs_HeadId");

            migrationBuilder.RenameIndex(
                name: "IX_TrainingSpec_ExId",
                table: "TrainingSpecs",
                newName: "IX_TrainingSpecs_ExId");

            migrationBuilder.AddColumn<byte>(
                name: "Amount",
                table: "TrainingProgramSpecs",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "Set",
                table: "TrainingProgramSpecs",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<float>(
                name: "Weight",
                table: "TrainingProgramSpecs",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TrainingProgramHeads",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrainingPlanHeadId",
                table: "TrainingHeads",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TrainingHeads",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrainigPlanId",
                table: "TrainingSpecs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainingSpecs",
                table: "TrainingSpecs",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TrainingPlanHeads",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 40, nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingPlanHeads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingPlanHeads_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrainingPlanSpecs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProgramHeadId = table.Column<int>(nullable: false),
                    ExId = table.Column<int>(nullable: false),
                    Weight = table.Column<float>(nullable: false),
                    Set = table.Column<byte>(nullable: false),
                    Amount = table.Column<byte>(nullable: false),
                    TrainingPlanHeadId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingPlanSpecs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingPlanSpecs_Exercises_ExId",
                        column: x => x.ExId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainingPlanSpecs_TrainingPlanHeads_TrainingPlanHeadId",
                        column: x => x.TrainingPlanHeadId,
                        principalTable: "TrainingPlanHeads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainingProgramHeads_UserId",
                table: "TrainingProgramHeads",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingHeads_TrainingPlanHeadId",
                table: "TrainingHeads",
                column: "TrainingPlanHeadId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingHeads_UserId",
                table: "TrainingHeads",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingSpecs_TrainigPlanId",
                table: "TrainingSpecs",
                column: "TrainigPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPlanHeads_UserId",
                table: "TrainingPlanHeads",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPlanSpecs_ExId",
                table: "TrainingPlanSpecs",
                column: "ExId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPlanSpecs_TrainingPlanHeadId",
                table: "TrainingPlanSpecs",
                column: "TrainingPlanHeadId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingHeads_TrainingPlanHeads_TrainingPlanHeadId",
                table: "TrainingHeads",
                column: "TrainingPlanHeadId",
                principalTable: "TrainingPlanHeads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingHeads_AspNetUsers_UserId",
                table: "TrainingHeads",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingProgramHeads_AspNetUsers_UserId",
                table: "TrainingProgramHeads",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingSpecs_TrainingPlanHeads_TrainigPlanId",
                table: "TrainingSpecs",
                column: "TrainigPlanId",
                principalTable: "TrainingPlanHeads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingHeads_TrainingPlanHeads_TrainingPlanHeadId",
                table: "TrainingHeads");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingHeads_AspNetUsers_UserId",
                table: "TrainingHeads");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingProgramHeads_AspNetUsers_UserId",
                table: "TrainingProgramHeads");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingSpecs_Exercises_ExId",
                table: "TrainingSpecs");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingSpecs_TrainingHeads_HeadId",
                table: "TrainingSpecs");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingSpecs_TrainingPlanHeads_TrainigPlanId",
                table: "TrainingSpecs");

            migrationBuilder.DropTable(
                name: "TrainingPlanSpecs");

            migrationBuilder.DropTable(
                name: "TrainingPlanHeads");

            migrationBuilder.DropIndex(
                name: "IX_TrainingProgramHeads_UserId",
                table: "TrainingProgramHeads");

            migrationBuilder.DropIndex(
                name: "IX_TrainingHeads_TrainingPlanHeadId",
                table: "TrainingHeads");

            migrationBuilder.DropIndex(
                name: "IX_TrainingHeads_UserId",
                table: "TrainingHeads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainingSpecs",
                table: "TrainingSpecs");

            migrationBuilder.DropIndex(
                name: "IX_TrainingSpecs_TrainigPlanId",
                table: "TrainingSpecs");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "TrainingProgramSpecs");

            migrationBuilder.DropColumn(
                name: "Set",
                table: "TrainingProgramSpecs");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "TrainingProgramSpecs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TrainingProgramHeads");

            migrationBuilder.DropColumn(
                name: "TrainingPlanHeadId",
                table: "TrainingHeads");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TrainingHeads");

            migrationBuilder.DropColumn(
                name: "TrainigPlanId",
                table: "TrainingSpecs");

            migrationBuilder.RenameTable(
                name: "TrainingSpecs",
                newName: "TrainingSpec");

            migrationBuilder.RenameColumn(
                name: "TrainigPlanHeadId",
                table: "TrainingHeads",
                newName: "UserProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_TrainingSpecs_HeadId",
                table: "TrainingSpec",
                newName: "IX_TrainingSpec_HeadId");

            migrationBuilder.RenameIndex(
                name: "IX_TrainingSpecs_ExId",
                table: "TrainingSpec",
                newName: "IX_TrainingSpec_ExId");

            migrationBuilder.AddColumn<byte>(
                name: "AmountPlan",
                table: "TrainingSpec",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainingSpec",
                table: "TrainingSpec",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Sets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SetAmount = table.Column<byte>(nullable: false),
                    SetNum = table.Column<byte>(nullable: false),
                    TrPrSpecId = table.Column<int>(nullable: false),
                    Weight = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sets_TrainingProgramSpecs_TrPrSpecId",
                        column: x => x.TrPrSpecId,
                        principalTable: "TrainingProgramSpecs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainingSchedulers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Day = table.Column<DateTime>(nullable: false),
                    ProfileId = table.Column<int>(nullable: false),
                    TrProgramId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingSchedulers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingSchedulers_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainingSchedulers_Exercises_TrProgramId",
                        column: x => x.TrProgramId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sets_TrPrSpecId",
                table: "Sets",
                column: "TrPrSpecId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingSchedulers_ProfileId",
                table: "TrainingSchedulers",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingSchedulers_TrProgramId",
                table: "TrainingSchedulers",
                column: "TrProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingSpec_Exercises_ExId",
                table: "TrainingSpec",
                column: "ExId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingSpec_TrainingHeads_HeadId",
                table: "TrainingSpec",
                column: "HeadId",
                principalTable: "TrainingHeads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
