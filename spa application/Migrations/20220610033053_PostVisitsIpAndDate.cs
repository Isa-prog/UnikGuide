using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Destinationosh.Migrations
{
    public partial class PostVisitsIpAndDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UrlReferrer",
                table: "PostVisits",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Ip",
                table: "PostVisits",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "VisitDate",
                table: "PostVisits",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ip",
                table: "PostVisits");

            migrationBuilder.DropColumn(
                name: "VisitDate",
                table: "PostVisits");

            migrationBuilder.UpdateData(
                table: "PostVisits",
                keyColumn: "UrlReferrer",
                keyValue: null,
                column: "UrlReferrer",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "UrlReferrer",
                table: "PostVisits",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
