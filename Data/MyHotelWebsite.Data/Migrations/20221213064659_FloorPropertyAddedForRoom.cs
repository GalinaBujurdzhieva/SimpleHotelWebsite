﻿#nullable disable

namespace MyHotelWebsite.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class FloorPropertyAddedForRoom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Floor",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Floor",
                table: "Rooms");
        }
    }
}
