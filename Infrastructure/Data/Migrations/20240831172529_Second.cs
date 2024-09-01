using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RouteId",
                table: "routes",
                newName: "route_id");

            migrationBuilder.RenameColumn(
                name: "DepartureTime",
                table: "flights",
                newName: "departure_time");

            migrationBuilder.RenameColumn(
                name: "ArrivalTime",
                table: "flights",
                newName: "arrival_time");

            migrationBuilder.RenameColumn(
                name: "AirlineId",
                table: "flights",
                newName: "airline_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "route_id",
                table: "routes",
                newName: "RouteId");

            migrationBuilder.RenameColumn(
                name: "departure_time",
                table: "flights",
                newName: "DepartureTime");

            migrationBuilder.RenameColumn(
                name: "arrival_time",
                table: "flights",
                newName: "ArrivalTime");

            migrationBuilder.RenameColumn(
                name: "airline_id",
                table: "flights",
                newName: "AirlineId");
        }
    }
}
