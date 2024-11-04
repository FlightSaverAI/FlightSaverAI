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
        public string? FlightNumber { get; set; }

        [Required]
        public int DepartureAirportId { get; set; }

        public virtual Airport? DepartureAirport { get; set; }

        [Required]
        public int ArrivalAirportId { get; set; }

        public virtual Airport? ArrivalAirport { get; set; }

        [Required]
        public int? AirlineId { get; set; }

        public virtual Airline? Airline { get; set; }

        [Required]
        public int? AircraftId { get; set; }

        public virtual Aircraft? Aircraft { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        public DateTime ArrivalTime { get; set; }

        public TimeSpan FlightDuration => ArrivalTime - DepartureTime;

        [Required]
        public ClassType ClassType { get; set; }

        [Required]
        public SeatType SeatType { get; set; }

        [Required]
        public string? SeatNumber { get; set; }

        [Required]
        public Reason Reason { get; set; }

        public AirportReview? DepartureAirportReview { get; set; }

        public AirportReview? ArrivalAirportReview { get; set; }

        public AirlineReview? AirlineReview { get; set; }

        public AircraftReview? AircraftReview { get; set; }

        [Required]
        public int UserId { get; set; }

        public virtual User? User { get; set; }
    }
}

