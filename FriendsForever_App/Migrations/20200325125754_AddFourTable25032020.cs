using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FriendsForever_App.Migrations
{
    public partial class AddFourTable25032020 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommentsMaster",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostedId = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    CommentedUserId = table.Column<string>(nullable: true),
                    CommentedTimeStamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentsMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LikesMaster",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostedId = table.Column<string>(nullable: true),
                    LikeUserId = table.Column<string>(nullable: true),
                    LikeTimeStamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikesMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostImagesMaster",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostedId = table.Column<string>(nullable: true),
                    PostedPhoto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostImagesMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostMaster",
                columns: table => new
                {
                    PostedId = table.Column<string>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostedContent = table.Column<string>(nullable: true),
                    PostedOwnerUserId = table.Column<string>(nullable: true),
                    PostedOwnerPhoto = table.Column<string>(nullable: true),
                    PostedOwnerName = table.Column<string>(nullable: true),
                    LikesCount = table.Column<int>(nullable: false),
                    CommentsCount = table.Column<int>(nullable: false),
                    PhotoAttached = table.Column<string>(nullable: true),
                    PostedTimeStamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostMaster", x => x.PostedId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentsMaster");

            migrationBuilder.DropTable(
                name: "LikesMaster");

            migrationBuilder.DropTable(
                name: "PostImagesMaster");

            migrationBuilder.DropTable(
                name: "PostMaster");
        }
    }
}
