using System;
using FlightSaverApi.Models.AirportModel;

namespace FlightSaverApi.Models.ReviewModel
{
    public class AirportReview : Review
    {
        public int AirportId { get; set; }
        public virtual Airport? Airport { get; set; }
    }
}

