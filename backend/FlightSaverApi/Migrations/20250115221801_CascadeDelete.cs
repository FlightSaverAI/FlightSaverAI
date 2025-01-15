using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightSaverApi.Migrations
{
    /// <inheritdoc />
    public partial class CascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AircraftReviews_Flights_FlightId",
                table: "AircraftReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_AirlineReviews_Flights_FlightId",
                table: "AirlineReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_AirportReviews_Flights_FlightId",
                table: "AirportReviews");

            migrationBuilder.AddForeignKey(
                name: "FK_AircraftReviews_Flights_FlightId",
                table: "AircraftReviews",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AirlineReviews_Flights_FlightId",
                table: "AirlineReviews",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AirportReviews_Flights_FlightId",
                table: "AirportReviews",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AircraftReviews_Flights_FlightId",
                table: "AircraftReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_AirlineReviews_Flights_FlightId",
                table: "AirlineReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_AirportReviews_Flights_FlightId",
                table: "AirportReviews");

            migrationBuilder.AddForeignKey(
                name: "FK_AircraftReviews_Flights_FlightId",
                table: "AircraftReviews",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AirlineReviews_Flights_FlightId",
                table: "AirlineReviews",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AirportReviews_Flights_FlightId",
                table: "AirportReviews",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "Id");
        }
    }
}
