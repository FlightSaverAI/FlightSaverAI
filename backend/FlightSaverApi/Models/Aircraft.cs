using FlightSaverApi.Models.ReviewModel;

namespace FlightSaverApi.Models
{
    public class Aircraft
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string IataCode { get; set; }

        public string IcaoCode { get; set; }

        public string RegNumber { get; set; }

        public string? AircraftUrl { get; set; }

        public int AirlineId { get; set; }

        public virtual Airline Airline { get; set; }

        public virtual List<AircraftReview>? AircraftReviews { get; set; }
        
        public virtual List<Flight>? Flights { get; set; }
    }
}
