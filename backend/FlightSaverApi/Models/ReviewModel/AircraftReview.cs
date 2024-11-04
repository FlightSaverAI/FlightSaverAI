using System;
using FlightSaverApi.Models.AircraftModel;

namespace FlightSaverApi.Models.ReviewModel
{
    public class AircraftReview : Review
    {
        public int AircraftId { get; set; }
        public virtual Aircraft? Aircraft { get; set; }
    }
}

