using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyHotelWebsite.Data.Migrations
{
    public partial class ReservationPhoneAndEmailChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservationEmail",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ReservationPhone",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ReservationEmail",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReservationPhone",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservationEmail",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ReservationPhone",
                table: "Reservations");

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
    }
}
