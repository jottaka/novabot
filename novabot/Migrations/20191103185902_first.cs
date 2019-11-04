using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NovaBot.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Configurations",
                columns: table => new
                {
                    ConfigurationId = table.Column<string>(nullable: false),
                    ClientId = table.Column<string>(maxLength: 70, nullable: true),
                    ClientSecret = table.Column<string>(maxLength: 70, nullable: true),
                    BotUserId = table.Column<string>(maxLength: 70, nullable: true),
                    BotAccessToken = table.Column<string>(maxLength: 70, nullable: true),
                    LastAuthToken = table.Column<string>(maxLength: 70, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configurations", x => x.ConfigurationId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Ranking = table.Column<long>(nullable: false),
                    SlackId = table.Column<string>(maxLength: 50, nullable: false),
                    RealName = table.Column<string>(maxLength: 200, nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    StatusText = table.Column<string>(maxLength: 500, nullable: true),
                    ProfilePicutre_72 = table.Column<string>(maxLength: 500, nullable: true),
                    ProfilePicture_192 = table.Column<string>(maxLength: 500, nullable: true),
                    ProfilePicutre_512 = table.Column<string>(maxLength: 500, nullable: true),
                    PasswordHash = table.Column<string>(maxLength: 400, nullable: true),
                    Salt = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Quote",
                columns: table => new
                {
                    QuoteId = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    SnitchId = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: false),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    Upvotes = table.Column<long>(nullable: false),
                    Downvotes = table.Column<long>(nullable: false),
                    QuoteVoteUid = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quote", x => x.QuoteId);
                    table.ForeignKey(
                        name: "FK_Quote_User_SnitchId",
                        column: x => x.SnitchId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Quote_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VoteModels",
                columns: table => new
                {
                    UserSlackId = table.Column<string>(nullable: false),
                    QuoteVoteUid = table.Column<string>(nullable: false),
                    Vote = table.Column<short>(nullable: false),
                    QuoteId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoteModels", x => new { x.UserSlackId, x.QuoteVoteUid });
                    table.ForeignKey(
                        name: "FK_VoteModels_Quote_QuoteId",
                        column: x => x.QuoteId,
                        principalTable: "Quote",
                        principalColumn: "QuoteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VoteModels_User_UserSlackId",
                        column: x => x.UserSlackId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Quote_SnitchId",
                table: "Quote",
                column: "SnitchId");

            migrationBuilder.CreateIndex(
                name: "IX_Quote_UserId",
                table: "Quote",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VoteModels_QuoteId",
                table: "VoteModels",
                column: "QuoteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Configurations");

            migrationBuilder.DropTable(
                name: "VoteModels");

            migrationBuilder.DropTable(
                name: "Quote");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
