using Microsoft.EntityFrameworkCore.Migrations;

namespace PerfectBuild.Migrations
{
    public partial class AddedOrderInTrainingPlanSpec : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "TrainingPlanSpecs",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "TrainingPlanSpecs");
        }
    }
}
