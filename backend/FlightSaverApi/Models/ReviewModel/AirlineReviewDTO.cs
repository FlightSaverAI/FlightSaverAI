using System;
using System.ComponentModel.DataAnnotations;

namespace FlightSaverApi.Models.ReviewModel
{
    public class AirlineReviewDTO : ReviewDTO
    {
        public int AirlineId { get; set; }
    }

}

