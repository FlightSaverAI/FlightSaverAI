using FlightSaverApi.Models.Review;

namespace FlightSaverApi.Models
{
    public class Airline
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string IataCode { get; set; }

        public string IcaoCode { get; set; }

        public string Country { get; set; }

        public string? LogoUrl { get; set; }

        public virtual List<Aircraft>? Aircrafts { get; set; }
        
        public virtual List<AirlineReview>? AirlineReviews { get; set; }
        
        public virtual List<Flight>? Flights { get; set; }
    }
}

