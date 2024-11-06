using System;
using System.ComponentModel.DataAnnotations;
using FlightSaverApi.Models.AirlineModel;

namespace FlightSaverApi.Models.ReviewModel
{
    public class AirlineReview : Review
    {
        public int AirlineId { get; set; }

        public Airline Airline { get; set; }
    }
}

