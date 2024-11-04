using System;
using FlightSaverApi.Models.AirlineModel;

namespace FlightSaverApi.Models.ReviewModel
{
    public class AirlineReview : Review
    {
        public int AirlineId { get; set; }
        public virtual Airline? Airline { get; set; }
    }
}

