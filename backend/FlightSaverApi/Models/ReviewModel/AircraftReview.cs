using System;
using System.ComponentModel.DataAnnotations;
using FlightSaverApi.Models.AircraftModel;
using FlightSaverApi.Models.FlightModel;
using FlightSaverApi.Models.UserModel;

namespace FlightSaverApi.Models.ReviewModel
{
    public class AircraftReview : Review
    {
        public int AircraftId { get; set; }

        public virtual Aircraft Aircraft { get; set; }
        
        public virtual Flight Flight { get; set; }
    }
}

