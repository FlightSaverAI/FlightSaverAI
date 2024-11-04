using System;
namespace FlightSaverApi.Models.ReviewModel
{
    public class AirportReviewDTO : ReviewDTO
    {
        [Required]
        public required int AirportId { get; set; }
    }
}

