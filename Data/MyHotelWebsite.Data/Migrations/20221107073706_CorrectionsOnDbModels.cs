using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyHotelWebsite.Data.Migrations
{
    public partial class CorrectionsOnDbModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Guests_GuestId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Staff_StaffId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Staff_StaffId",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_Staff_StaffId",
                table: "Dishes");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Staff_StaffId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Staff_StaffId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Staff_StaffId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_GuestId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_StaffId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Staff",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "StaffId",
                table: "Rooms",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCleaned",
                table: "Rooms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "StaffId",
                table: "Reservations",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StaffId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Guests",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "StaffId",
                table: "Dishes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsReady",
                table: "Dishes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "StaffId",
                table: "Blogs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StaffId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GuestId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Staff_ApplicationUserId",
                table: "Staff",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Guests_ApplicationUserId",
                table: "Guests",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Staff_StaffId",
                table: "Blogs",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_Staff_StaffId",
                table: "Dishes",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Guests_AspNetUsers_ApplicationUserId",
                table: "Guests",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Staff_StaffId",
                table: "Orders",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Staff_StaffId",
                table: "Reservations",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Staff_StaffId",
                table: "Rooms",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_AspNetUsers_ApplicationUserId",
                table: "Staff",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Staff_StaffId",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_Staff_StaffId",
                table: "Dishes");

            migrationBuilder.DropForeignKey(
                name: "FK_Guests_AspNetUsers_ApplicationUserId",
                table: "Guests");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Staff_StaffId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Staff_StaffId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Staff_StaffId",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Staff_AspNetUsers_ApplicationUserId",
                table: "Staff");

            migrationBuilder.DropIndex(
                name: "IX_Staff_ApplicationUserId",
                table: "Staff");

            migrationBuilder.DropIndex(
                name: "IX_Guests_ApplicationUserId",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "IsCleaned",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "IsReady",
                table: "Dishes");

            migrationBuilder.AlterColumn<string>(
                name: "StaffId",
                table: "Rooms",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "StaffId",
                table: "Reservations",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "StaffId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "StaffId",
                table: "Dishes",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "StaffId",
                table: "Blogs",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "StaffId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GuestId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GuestId",
                table: "AspNetUsers",
                column: "GuestId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_StaffId",
                table: "AspNetUsers",
                column: "StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Guests_GuestId",
                table: "AspNetUsers",
                column: "GuestId",
                principalTable: "Guests",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Staff_StaffId",
                table: "AspNetUsers",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Staff_StaffId",
                table: "Blogs",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_Staff_StaffId",
                table: "Dishes",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Staff_StaffId",
                table: "Orders",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Staff_StaffId",
                table: "Reservations",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Staff_StaffId",
                table: "Rooms",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "Id");
        }
    }
}
