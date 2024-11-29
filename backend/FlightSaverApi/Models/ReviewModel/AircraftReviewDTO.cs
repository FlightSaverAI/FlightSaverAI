using System;
using System.ComponentModel.DataAnnotations;

namespace FlightSaverApi.Models.ReviewModel
{
    public class AircraftReviewDTO : ReviewDTO
    {
        public int AircraftId { get; set; }
    }
}

