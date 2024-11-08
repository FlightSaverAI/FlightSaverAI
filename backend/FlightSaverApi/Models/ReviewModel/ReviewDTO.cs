using System;
using System.ComponentModel.DataAnnotations;

namespace FlightSaverApi.Models.ReviewModel
{
    public abstract class ReviewDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int Rating { get; set; }

        public string? Comment { get; set; }
        
        public int FlightId { get; set; }
    }
}

