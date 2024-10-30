using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Destinationosh.Migrations
{
    public partial class PostRefactoredAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "En",
                table: "Posts",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Kg",
                table: "Posts",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Ru",
                table: "Posts",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "En",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Kg",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Ru",
                table: "Posts");
        }
    }
}
