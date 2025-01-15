using FlightSaverApi.DTOs.Aircraft;
using FlightSaverApi.DTOs.Airline;
using FlightSaverApi.DTOs.Airport;
using FlightSaverApi.DTOs.Review;
using FlightSaverApi.Enums.FlightEnums;

namespace FlightSaverApi.DTOs.Flight
{
    public class FlightDTO
    {
        public int Id { get; set; }

        public string? FlightNumber { get; set; }

        public int DepartureAirportId { get; set; }
        
        public AirportDTO? DepartureAirport { get; set; }

        public int ArrivalAirportId { get; set; }
        
        public AirportDTO? ArrivalAirport { get; set; }

        public int? AirlineId { get; set; }
        
        public AirlineDTO? Airline { get; set; }

        public int? AircraftId { get; set; }
        
        public AircraftDTO? Aircraft { get; set; }

        public DateTime DepartureTime { get; set; }

        public DateTime ArrivalTime { get; set; }

        public ClassType ClassType { get; set; }

        public SeatType SeatType { get; set; }

        public string SeatNumber { get; set; }

        public Reason Reason { get; set; }

        public int? UserId { get; set; }

        public List<AirportReviewDTO>? AirportReviews { get; set; }

        public AirlineReviewDTO? AirlineReview { get; set; }

        public AircraftReviewDTO? AircraftReview { get; set; }
    }
}

