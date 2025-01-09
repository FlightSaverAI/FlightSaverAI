using System;
using System.ComponentModel.DataAnnotations;
using FlightSaverApi.Attributes;

namespace FlightSaverApi.Models.ReviewModel
{
    public class AircraftReview : Review
    {
        public int AircraftId { get; set; }

        public virtual Aircraft Aircraft { get; set; }
    }
}

