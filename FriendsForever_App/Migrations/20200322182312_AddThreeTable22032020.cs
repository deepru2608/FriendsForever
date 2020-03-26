using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FriendsForever_App.Migrations
{
    public partial class AddThreeTable22032020 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    FeedBackId = table.Column<string>(nullable: false),
                    SerialNo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    QualityStatus = table.Column<int>(nullable: false),
                    FeedbackMessage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.FeedBackId);
                });

            migrationBuilder.CreateTable(
                name: "LogTableForLogin",
                columns: table => new
                {
                    LogId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    Ip_Address = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    StatusFlag = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogTableForLogin", x => x.LogId);
                    table.ForeignKey(
                        name: "FK_LogTableForLogin_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QualityParameters",
                columns: table => new
                {
                    ParamId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParamMessage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualityParameters", x => x.ParamId);
                });

            migrationBuilder.InsertData(
                table: "QualityParameters",
                columns: new[] { "ParamId", "ParamMessage" },
                values: new object[,]
                {
                    { 1, "Bad" },
                    { 2, "Average" },
                    { 3, "Good" },
                    { 4, "Best" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LogTableForLogin_ApplicationUserId",
                table: "LogTableForLogin",
                column: "ApplicationUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "LogTableForLogin");

            migrationBuilder.DropTable(
                name: "QualityParameters");
        }
    }
}
