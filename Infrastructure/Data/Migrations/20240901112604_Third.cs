using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RouteId",
                table: "subscriptions",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_subscriptions",
                table: "subscriptions",
                columns: new[] { "agency_id", "origin_city_id", "destination_city_id" });

            migrationBuilder.CreateIndex(
                name: "IX_subscriptions_agency_id",
                table: "subscriptions",
                column: "agency_id");

            migrationBuilder.CreateIndex(
                name: "IX_subscriptions_origin_city_id_destination_city_id",
                table: "subscriptions",
                columns: new[] { "origin_city_id", "destination_city_id" });

            migrationBuilder.CreateIndex(
                name: "IX_subscriptions_RouteId",
                table: "subscriptions",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_routes_origin_city_id_destination_city_id",
                table: "routes",
                columns: new[] { "origin_city_id", "destination_city_id" });

            migrationBuilder.CreateIndex(
                name: "IX_flights_departure_time",
                table: "flights",
                column: "departure_time");

            migrationBuilder.AddForeignKey(
                name: "FK_subscriptions_routes_RouteId",
                table: "subscriptions",
                column: "RouteId",
                principalTable: "routes",
                principalColumn: "route_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_subscriptions_routes_RouteId",
                table: "subscriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_subscriptions",
                table: "subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_subscriptions_agency_id",
                table: "subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_subscriptions_origin_city_id_destination_city_id",
                table: "subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_subscriptions_RouteId",
                table: "subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_routes_origin_city_id_destination_city_id",
                table: "routes");

            migrationBuilder.DropIndex(
                name: "IX_flights_departure_time",
                table: "flights");

            migrationBuilder.DropColumn(
                name: "RouteId",
                table: "subscriptions");
        }
    }
}
