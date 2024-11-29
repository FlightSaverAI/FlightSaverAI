using System;
using System.ComponentModel.DataAnnotations;
using FlightSaverApi.Enums;

namespace FlightSaverApi.Models.ReviewModel
{
    public class AirportReviewDTO : ReviewDTO
    {
        public AirportReviewType AirportReviewType { get; set; }
        
        public int AirportId { get; set; }
    }
}

