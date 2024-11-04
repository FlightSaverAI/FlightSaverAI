using System;
using System.ComponentModel.DataAnnotations;
using FlightSaverApi.Models.AirportModel;

namespace FlightSaverApi.Models.ReviewModel
{
    public class AirportReview : Review
    {
        [Required]
        public required int AirportId { get; set; }

        public virtual Airport? Airport { get; set; }
    }
}

