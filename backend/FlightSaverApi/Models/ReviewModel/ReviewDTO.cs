using System;
using System.ComponentModel.DataAnnotations;

namespace FlightSaverApi.Models.ReviewModel
{
    public abstract class ReviewDTO
    {
        public int Id { get; set; }

        public int ReviewerId { get; set; }

        public int Rating { get; set; }

        public string? Comment { get; set; }
    }
}

