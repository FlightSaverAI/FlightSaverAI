using FlightSaverApi.Enums;

namespace FlightSaverApi.DTOs.Review
{
    public class AirportReviewDTO : ReviewDTO
    {
        public AirportReviewType AirportReviewType { get; set; }
        
        public int AirportId { get; set; }
    }
}

