using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cryptocop.Software.API.Migrations
{
    public partial class FixedSpellingErrorInTableName2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_JwTokens",
                table: "JwTokens");

            migrationBuilder.RenameTable(
                name: "JwTokens",
                newName: "JwtTokens");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JwtTokens",
                table: "JwtTokens",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_JwtTokens",
                table: "JwtTokens");

            migrationBuilder.RenameTable(
                name: "JwtTokens",
                newName: "JwTokens");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JwTokens",
                table: "JwTokens",
                column: "Id");
        }
    }
}
