using Microsoft.EntityFrameworkCore.Migrations;

namespace FreshFruits.Data.Migrations
{
    public partial class AddRatingAndPriceToFruit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Fruits",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Fruits",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Fruits");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Fruits");
        }
    }
}
