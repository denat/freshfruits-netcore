using Microsoft.EntityFrameworkCore.Migrations;

namespace FreshFruits.Data.Migrations
{
    public partial class DataSeedingForPriceAndRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Fruits",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Price", "Rating" },
                values: new object[] { 2.99m, 4 });

            migrationBuilder.UpdateData(
                table: "Fruits",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Price", "Rating" },
                values: new object[] { 1.99m, 3 });

            migrationBuilder.UpdateData(
                table: "Fruits",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Price", "Rating" },
                values: new object[] { 5.99m, 5 });

            migrationBuilder.UpdateData(
                table: "Fruits",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Price", "Rating" },
                values: new object[] { 4.99m, 4 });

            migrationBuilder.UpdateData(
                table: "Fruits",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Price", "Rating" },
                values: new object[] { 5.99m, 5 });

            migrationBuilder.UpdateData(
                table: "Fruits",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Price", "Rating" },
                values: new object[] { 9.99m, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Fruits",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Price", "Rating" },
                values: new object[] { 0m, 0 });

            migrationBuilder.UpdateData(
                table: "Fruits",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Price", "Rating" },
                values: new object[] { 0m, 0 });

            migrationBuilder.UpdateData(
                table: "Fruits",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Price", "Rating" },
                values: new object[] { 0m, 0 });

            migrationBuilder.UpdateData(
                table: "Fruits",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Price", "Rating" },
                values: new object[] { 0m, 0 });

            migrationBuilder.UpdateData(
                table: "Fruits",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Price", "Rating" },
                values: new object[] { 0m, 0 });

            migrationBuilder.UpdateData(
                table: "Fruits",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Price", "Rating" },
                values: new object[] { 0m, 0 });
        }
    }
}
