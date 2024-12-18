﻿using System;
using System.ComponentModel.DataAnnotations;
using FlightSaverApi.Attributes;
using FlightSaverApi.Models.AirlineModel;
using FlightSaverApi.Models.FlightModel;

namespace FlightSaverApi.Models.ReviewModel
{
    public class AirlineReview : Review
    {
        public int AirlineId { get; set; }

        public virtual Airline Airline { get; set; }
    }
}

