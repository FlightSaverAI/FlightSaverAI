using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightSaverApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAircraftModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Secret",
                table: "Aircrafts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Secret",
                table: "Aircrafts",
                type: "text",
                nullable: true);
        }
    }
}
