using System;
using System.ComponentModel.DataAnnotations;
using FlightSaverApi.Attributes;
using FlightSaverApi.Enums;
using FlightSaverApi.Models.AirportModel;
using FlightSaverApi.Models.FlightModel;
using Newtonsoft.Json;

namespace FlightSaverApi.Models.ReviewModel
{ 
    [SwaggerExclude]
    public class AirportReview : Review
    {
        public AirportReviewType AirportReviewType { get; set; }
        
        public int AirportId { get; set; }
        
        public virtual Airport Airport { get; set; }
    }
}

