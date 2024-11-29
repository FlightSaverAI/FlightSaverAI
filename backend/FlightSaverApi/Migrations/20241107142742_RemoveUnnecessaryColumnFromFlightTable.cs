using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightSaverApi.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUnnecessaryColumnFromFlightTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AircraftReviewId",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "DepartureAirportReviewId",
                table: "Flights");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AircraftReviewId",
                table: "Flights",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepartureAirportReviewId",
                table: "Flights",
                type: "integer",
                nullable: true);
        }
    }
}
