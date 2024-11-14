using System;
using System.ComponentModel.DataAnnotations;
using FlightSaverApi.Attributes;
using FlightSaverApi.Models.FlightModel;
using FlightSaverApi.Models.UserModel;
using Newtonsoft.Json;

namespace FlightSaverApi.Models.ReviewModel
{
    [SwaggerExclude]
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

