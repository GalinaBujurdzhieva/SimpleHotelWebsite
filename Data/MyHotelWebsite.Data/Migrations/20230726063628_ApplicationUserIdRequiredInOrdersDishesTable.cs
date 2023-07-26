using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyHotelWebsite.Data.Migrations
{
    public partial class ApplicationUserIdRequiredInOrdersDishesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DishesOrders_AspNetUsers_ApplicationUserId",
                table: "DishesOrders");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "DishesOrders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DishesOrders_AspNetUsers_ApplicationUserId",
                table: "DishesOrders",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DishesOrders_AspNetUsers_ApplicationUserId",
                table: "DishesOrders");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "DishesOrders",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_DishesOrders_AspNetUsers_ApplicationUserId",
                table: "DishesOrders",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
