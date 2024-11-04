using System;
using System.ComponentModel.DataAnnotations;

namespace FlightSaverApi.Models.ReviewModel
{
    public abstract class ReviewDTO
    {
        public int Id { get; set; }

        [Required]
        public required int ReviewerId { get; set; }

        [Required]
        public required int Rating { get; set; }

        public string? Comment { get; set; }
    }
}

