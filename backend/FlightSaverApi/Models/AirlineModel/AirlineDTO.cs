using System;
using System.ComponentModel.DataAnnotations;

namespace FlightSaverApi.Models.AirlineModel
{
    public class AirlineDTO
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
        public required string Country { get; set; }

        public string? LogoUrl { get; set; }
    }
}

