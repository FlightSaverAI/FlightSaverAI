using System;
using System.ComponentModel.DataAnnotations;

namespace FlightSaverApi.Models.AirportModel
{
    public class Airport
    {
        public int Id { get; set; }
        [Required]
        [StringLength(4)]
        public string? IcaoCode { get; set; }

        [Required]
        [StringLength(3)]
        public string? IataCode { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? City { get; set; }

        [Required]
        public string? Country { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }
    }
}

