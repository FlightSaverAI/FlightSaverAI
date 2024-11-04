﻿using System;
using System.ComponentModel.DataAnnotations;
using FlightSaverApi.Models.AircraftModel;

namespace FlightSaverApi.Models.ReviewModel
{
    public class AircraftReview : Review
    {
        [Required]
        public required int AircraftId { get; set; }

        public virtual Aircraft? Aircraft { get; set; }
    }
}

