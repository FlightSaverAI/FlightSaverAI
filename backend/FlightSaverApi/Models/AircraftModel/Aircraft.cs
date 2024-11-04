using System.ComponentModel.DataAnnotations;
using FlightSaverApi.Models.AirlineModel;

namespace FlightSaverApi.Models.AircraftModel
{
    public class Aircraft
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

        public virtual Airline? Airline { get; set; }
    }
}
