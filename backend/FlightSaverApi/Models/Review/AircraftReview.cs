namespace FlightSaverApi.Models.Review
{
    public class AircraftReview : Review
    {
        public int AircraftId { get; set; }

        public virtual Aircraft Aircraft { get; set; }
    }
}

