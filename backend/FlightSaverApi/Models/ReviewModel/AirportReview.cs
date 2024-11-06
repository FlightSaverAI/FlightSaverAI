using System;
using System.ComponentModel.DataAnnotations;
using FlightSaverApi.Models.AirportModel;

namespace FlightSaverApi.Models.ReviewModel
{
    public class AirportReview : Review
    {
        public int AirportId { get; set; }

        public Airport Airport { get; set; }
    }
}

