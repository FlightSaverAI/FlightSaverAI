using System;
namespace FlightSaverApi.Models.ReviewModel
{
    public abstract class Review
    {
        public int Id { get; set; }
        public int ReviewerId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}

