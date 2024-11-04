using System;
using System.ComponentModel.DataAnnotations;

namespace FlightSaverApi.Models.ReviewModel
{
    public class AircraftReviewDTO : ReviewDTO
    {
        [Required]
        public required int AircraftId { get; set; }
    }
}

