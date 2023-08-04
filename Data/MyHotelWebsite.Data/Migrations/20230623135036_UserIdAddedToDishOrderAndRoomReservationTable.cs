#nullable disable
namespace MyHotelWebsite.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class UserIdAddedToDishOrderAndRoomReservationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "RoomsReservations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "DishesOrders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoomsReservations_ApplicationUserId",
                table: "RoomsReservations",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DishesOrders_ApplicationUserId",
                table: "DishesOrders",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DishesOrders_AspNetUsers_ApplicationUserId",
                table: "DishesOrders",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomsReservations_AspNetUsers_ApplicationUserId",
                table: "RoomsReservations",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DishesOrders_AspNetUsers_ApplicationUserId",
                table: "DishesOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomsReservations_AspNetUsers_ApplicationUserId",
                table: "RoomsReservations");

            migrationBuilder.DropIndex(
                name: "IX_RoomsReservations_ApplicationUserId",
                table: "RoomsReservations");

            migrationBuilder.DropIndex(
                name: "IX_DishesOrders_ApplicationUserId",
                table: "DishesOrders");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "RoomsReservations");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "DishesOrders");
        }
    }
}
