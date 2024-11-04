﻿using System;
using System.ComponentModel.DataAnnotations;
using FlightSaverApi.Models.AirlineModel;

namespace FlightSaverApi.Models.ReviewModel
{
    public class AirlineReview : Review
    {
        [Required]
        public required int AirlineId { get; set; }

        public virtual Airline? Airline { get; set; }
    }
}

