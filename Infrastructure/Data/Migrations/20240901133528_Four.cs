using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Four : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_subscriptions_routes_RouteId",
                table: "subscriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_subscriptions",
                table: "subscriptions");

            migrationBuilder.AlterColumn<int>(
                name: "RouteId",
                table: "subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_subscriptions_routes_RouteId",
                table: "subscriptions",
                column: "RouteId",
                principalTable: "routes",
                principalColumn: "route_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_subscriptions_routes_RouteId",
                table: "subscriptions");

            migrationBuilder.AlterColumn<int>(
                name: "RouteId",
                table: "subscriptions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_subscriptions",
                table: "subscriptions",
                columns: new[] { "agency_id", "origin_city_id", "destination_city_id" });

            migrationBuilder.AddForeignKey(
                name: "FK_subscriptions_routes_RouteId",
                table: "subscriptions",
                column: "RouteId",
                principalTable: "routes",
                principalColumn: "route_id");
        }
    }
}
