using System;
using System.ComponentModel.DataAnnotations;
using FlightSaverApi.Attributes;
using FlightSaverApi.Enums.FlightEnums;
using FlightSaverApi.Models.AircraftModel;
using FlightSaverApi.Models.AirlineModel;
using FlightSaverApi.Models.AirportModel;
using FlightSaverApi.Models.ReviewModel;
using FlightSaverApi.Models.UserModel;

namespace FlightSaverApi.Models.FlightModel
{
    [SwaggerExclude]
    public class Flight
    {
        public int Id { get; set; }

        public string FlightNumber { get; set; }

        public int DepartureAirportId { get; set; }

        public virtual Airport DepartureAirport { get; set; }

        public int ArrivalAirportId { get; set; }

        public virtual Airport ArrivalAirport { get; set; }

        public int AirlineId { get; set; }

        public virtual Airline Airline { get; set; }

        public int AircraftId { get; set; }

        public virtual Aircraft Aircraft { get; set; }

        public DateTime DepartureTime { get; set; }

        public DateTime ArrivalTime { get; set; }

        public TimeSpan FlightDuration => ArrivalTime - DepartureTime;

        public ClassType ClassType { get; set; }

        public SeatType SeatType { get; set; }

        public string SeatNumber { get; set; }

        public Reason Reason { get; set; }

        public virtual List<AirportReview> AirportReviews { get; set; }

        public virtual AirlineReview? AirlineReview { get; set; }

        public virtual AircraftReview? AircraftReview { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}

