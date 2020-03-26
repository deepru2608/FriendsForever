using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FriendsForever_App.Migrations
{
    public partial class AlterColumn22032020 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Doe",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<DateTime>(
                name: "Dob",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dob",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Doe",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
