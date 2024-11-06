using System;
using System.ComponentModel.DataAnnotations;

namespace FlightSaverApi.Models.ReviewModel
{
    public class AirportReviewDTO : ReviewDTO
    {
        public int AirportId { get; set; }
    }
}

