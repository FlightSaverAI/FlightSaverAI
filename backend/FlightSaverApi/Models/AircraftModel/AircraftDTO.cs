using System.ComponentModel.DataAnnotations;

namespace FlightSaverApi.Models.AircraftModel
{
    public class AircraftDTO
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        [StringLength(3)]
        public required string IataCode { get; set; }

        [Required]
        [StringLength(4)]
        public required string IcaoCode { get; set; }

        [Required]
        public required string RegNumber { get; set; }

        public string? AircraftUrl { get; set; }

        [Required]
        public required int AirlineId { get; set; }
    }
}
