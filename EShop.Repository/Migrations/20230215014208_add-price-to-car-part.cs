using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShop.Repository.Migrations
{
    public partial class addpricetocarpart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "ShoppingCartContainsCarParts");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "OrderContainsCarParts");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "CarParts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "CarParts");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "ShoppingCartContainsCarParts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "OrderContainsCarParts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
