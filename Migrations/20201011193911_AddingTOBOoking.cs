using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HorizonHotelWebsite.Migrations
{
    public partial class AddingTOBOoking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BookingPlaced",
                table: "Bookings",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookingPlaced",
                table: "Bookings");
        }
    }
}
