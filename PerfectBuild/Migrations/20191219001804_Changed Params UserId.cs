using Microsoft.EntityFrameworkCore.Migrations;

namespace PerfectBuild.Migrations
{
    public partial class ChangedParamsUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserParam_Profiles_ProfileId",
                table: "UserParam");

            migrationBuilder.DropIndex(
                name: "IX_UserParam_ProfileId",
                table: "UserParam");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "UserParam");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "UserParam",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserParam_UserId",
                table: "UserParam",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserParam_AspNetUsers_UserId",
                table: "UserParam",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserParam_AspNetUsers_UserId",
                table: "UserParam");

            migrationBuilder.DropIndex(
                name: "IX_UserParam_UserId",
                table: "UserParam");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserParam");

            migrationBuilder.AddColumn<int>(
                name: "ProfileId",
                table: "UserParam",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserParam_ProfileId",
                table: "UserParam",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserParam_Profiles_ProfileId",
                table: "UserParam",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
