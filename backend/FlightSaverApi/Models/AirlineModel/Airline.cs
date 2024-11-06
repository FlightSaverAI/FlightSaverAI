using System;
using System.ComponentModel.DataAnnotations;
using FlightSaverApi.Models.AircraftModel;

namespace FlightSaverApi.Models.AirlineModel
{
    public class Airline
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string IataCode { get; set; }

        public string IcaoCode { get; set; }

        public string Country { get; set; }

        public string? LogoUrl { get; set; }

        public virtual ICollection<Aircraft>? Aircrafts { get; set; }
    }
}

