﻿namespace FlightSaverApi.Models.Review
{
    public abstract class Review
    {
        public int Id { get; set; }

        public int Rating { get; set; }

        public string? Comment { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }
        
        public int FlightId { get; set; }
        
        public virtual Flight Flight { get; set; }
    }
}

