using System;
using System.ComponentModel.DataAnnotations;

namespace FlightSaverApi.Models.AirportModel
{
    public class Airport
    {
        public int Id { get; set; }

        [Required]
        [StringLength(4)]
        public required string IcaoCode { get; set; }

        [Required]
        [StringLength(3)]
        public required string IataCode { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string City { get; set; }

        [Required]
        public required string Country { get; set; }

        [Required]
        public required double Latitude { get; set; }

        [Required]
        public required double Longitude { get; set; }
    }
}

