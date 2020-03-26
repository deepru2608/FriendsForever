using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FriendsForever_App.Migrations
{
    public partial class AddInterestTable24032020 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                nullable: true,
                computedColumnSql: "[FirstName] + ' ' + [LastName]",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComputedColumnSql: "[LastName] + ', ' + [FirstName]");

            migrationBuilder.CreateTable(
                name: "UserInterestMaster",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InterestName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    Added_Time_Stamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInterestMaster", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserInterestMaster");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                computedColumnSql: "[LastName] + ', ' + [FirstName]",
                oldClrType: typeof(string),
                oldNullable: true,
                oldComputedColumnSql: "[FirstName] + ' ' + [LastName]");
        }
    }
}
