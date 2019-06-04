using Microsoft.EntityFrameworkCore.Migrations;

namespace FreshFruits.Data.Migrations
{
    public partial class RemoveUserSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "a18be9c0-aa65-4af8-bd17-00bd9344e575", "40a5e36e-92d7-42fd-aec9-6cbb4d014891" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a18be9c0-aa65-4af8-bd17-00bd9344e575", 0, "40a5e36e-92d7-42fd-aec9-6cbb4d014891", "admin@test.com", true, false, null, "admin@test.com", "admin@test.com", "AQAAAAEAACcQAAAAEFOjH2em36T+NoVJjaAXtJicxtVCyM4XCrrj5ubadNwmNaV7Bu22rLP/19LtzjNj0A==", null, false, "", false, "admin@test.com" });
        }
    }
}
