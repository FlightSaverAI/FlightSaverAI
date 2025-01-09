using System;
using System.ComponentModel.DataAnnotations;
using FlightSaverApi.Attributes;
using FlightSaverApi.Enums;
using Newtonsoft.Json;

namespace FlightSaverApi.Models.ReviewModel
{ 
    public class AirportReview : Review
    {
        public AirportReviewType AirportReviewType { get; set; }
        
        public int AirportId { get; set; }
        
        public virtual Airport Airport { get; set; }
    }
}

