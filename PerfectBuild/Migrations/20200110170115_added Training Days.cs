using Microsoft.EntityFrameworkCore.Migrations;

namespace PerfectBuild.Migrations
{
    public partial class addedTrainingDays : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "TrainingDays",
                table: "TrainingPlanHeads",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrainingDays",
                table: "TrainingPlanHeads");
        }
    }
}
