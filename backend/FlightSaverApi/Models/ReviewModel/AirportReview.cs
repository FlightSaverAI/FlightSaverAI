﻿using System;
using System.ComponentModel.DataAnnotations;
using FlightSaverApi.Enums;
using FlightSaverApi.Models.AirportModel;
using FlightSaverApi.Models.FlightModel;

namespace FlightSaverApi.Models.ReviewModel
{
    public class AirportReview : Review
    {
        public AirportReviewType AirportReviewType { get; set; }
        
        public int AirportId { get; set; }

        public virtual Airport Airport { get; set; }
        
        public virtual Flight Flight { get; set; }
    }
}

