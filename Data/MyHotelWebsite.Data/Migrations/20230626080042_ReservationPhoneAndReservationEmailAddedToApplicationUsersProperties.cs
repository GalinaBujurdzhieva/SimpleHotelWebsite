#nullable disable

namespace MyHotelWebsite.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ReservationPhoneAndReservationEmailAddedToApplicationUsersProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReservationEmail",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReservationPhone",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservationEmail",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ReservationPhone",
                table: "AspNetUsers");
        }
    }
}
