using FlightSaverApi.Enums;

namespace FlightSaverApi.Models.Review
{ 
    public class AirportReview : Review
    {
        public AirportReviewType AirportReviewType { get; set; }
        
        public int AirportId { get; set; }
        
        public virtual Airport Airport { get; set; }
    }
}

