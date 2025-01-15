namespace FlightSaverApi.Models.Review
{
    public class AirlineReview : Review
    {
        public int? AirlineId { get; set; }

        public virtual Airline Airline { get; set; }
    }
}

