using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyHotelWebsite.Data.Migrations
{
    public partial class IsShoppingCartAddedToAFinalOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsShoppingCartAddedToAFinalOrder",
                table: "ShoppingCarts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsShoppingCartAddedToAFinalOrder",
                table: "ShoppingCarts");
        }
    }
}
