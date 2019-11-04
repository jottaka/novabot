using Microsoft.EntityFrameworkCore.Migrations;

namespace NovaBot.Migrations
{
    public partial class secod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoteModels_User_UserSlackId",
                table: "VoteModels");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "VoteModels",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VoteModels_UserId",
                table: "VoteModels",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_VoteModels_User_UserId",
                table: "VoteModels",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoteModels_User_UserId",
                table: "VoteModels");

            migrationBuilder.DropIndex(
                name: "IX_VoteModels_UserId",
                table: "VoteModels");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "VoteModels");

            migrationBuilder.AddForeignKey(
                name: "FK_VoteModels_User_UserSlackId",
                table: "VoteModels",
                column: "UserSlackId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
