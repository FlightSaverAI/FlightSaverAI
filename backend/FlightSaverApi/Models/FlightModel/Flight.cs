using System;
using System.ComponentModel.DataAnnotations;
using FlightSaverApi.Enums.FlightEnums;
using FlightSaverApi.Models.AircraftModel;
using FlightSaverApi.Models.AirlineModel;
using FlightSaverApi.Models.AirportModel;
using FlightSaverApi.Models.ReviewModel;
using FlightSaverApi.Models.UserModel;

namespace FlightSaverApi.Models.FlightModel
{
    public class Flight
    {
        public int Id { get; set; }

        [Required]
        public required string FlightNumber { get; set; }

        [Required]
        public required int DepartureAirportId { get; set; }

        public virtual Airport? DepartureAirport { get; set; }

        [Required]
        public required int ArrivalAirportId { get; set; }

        public virtual Airport? ArrivalAirport { get; set; }

        [Required]
        public required int AirlineId { get; set; }

        public virtual Airline? Airline { get; set; }

        [Required]
        public required int AircraftId { get; set; }

        public virtual Aircraft? Aircraft { get; set; }

        [Required]
        public required DateTime DepartureTime { get; set; }

        [Required]
        public required DateTime ArrivalTime { get; set; }

        public TimeSpan FlightDuration => ArrivalTime - DepartureTime;

        [Required]
        public required ClassType ClassType { get; set; }

        [Required]
        public required SeatType SeatType { get; set; }

        [Required]
        public required string SeatNumber { get; set; }

        [Required]
        public required Reason Reason { get; set; }

        public AirportReview? DepartureAirportReview { get; set; }

        public AirportReview? ArrivalAirportReview { get; set; }

        public AirlineReview? AirlineReview { get; set; }

        public AircraftReview? AircraftReview { get; set; }

        [Required]
        public required int UserId { get; set; }

        public virtual User? User { get; set; }
    }
}

