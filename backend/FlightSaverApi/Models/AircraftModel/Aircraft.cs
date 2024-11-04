using System.ComponentModel.DataAnnotations;
using FlightSaverApi.Models.AirlineModel;

namespace FlightSaverApi.Models.AircraftModel
{
    public class Aircraft
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        [StringLength(3)]
        public string? IataCode { get; set; }
        [Required]
        [StringLength(4)]
        public string? IcaoCode { get; set; }
        [Required]
        public string? RegNumber { get; set; }
        public Airline? Airline { get; set; }
    }
}
