using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Destinationosh.Migrations
{
    public partial class FixMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "64e3acc8-85d5-4a0f-be1c-8b3d3f1db509");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "67992769-5a44-4cda-ba16-586b448150bd");

            migrationBuilder.DropColumn(
                name: "Login",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "AspNetUsers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "Login", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Role", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "64e3acc8-85d5-4a0f-be1c-8b3d3f1db509", 0, "b63df91a-2813-408c-a9e1-65fa62ad0289", "hey@mail.ru", false, "Miyuki Shirogane", false, null, "asddsda", null, null, null, null, false, "Admin", "c7c9aeb3-0f91-45d4-91e4-f32ca31c0108", false, null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "Login", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Role", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "67992769-5a44-4cda-ba16-586b448150bd", 0, "11959804-493e-4b2f-a3ac-33a279599125", "asd@gmail.com", false, "Yo Ishigami", false, null, "qwer", null, null, null, null, false, "Editor", "0a17c374-0deb-4d8c-ad87-18f2ae06b1f7", false, null });
        }
    }
}
