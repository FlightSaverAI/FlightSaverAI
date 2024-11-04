using System;
using System.ComponentModel.DataAnnotations;

namespace FlightSaverApi.Models.ReviewModel
{
    public class AirlineReviewDTO : ReviewDTO
    {
        [Required]
        public required int AirlineId { get; set; }
    }

}

