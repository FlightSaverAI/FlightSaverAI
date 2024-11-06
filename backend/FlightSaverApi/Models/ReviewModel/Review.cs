using System;
using System.ComponentModel.DataAnnotations;
using FlightSaverApi.Models.UserModel;

namespace FlightSaverApi.Models.ReviewModel
{
    public abstract class Review
    {
        public int Id { get; set; }

        public int Rating { get; set; }

        public string? Comment { get; set; }

        public int ReviewerId { get; set; }

        public virtual User Reviewer { get; set; }
    }
}

