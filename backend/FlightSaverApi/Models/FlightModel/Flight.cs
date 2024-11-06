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

        public string FlightNumber { get; set; }

        public int DepartureAirportId { get; set; }

        public Airport DepartureAirport { get; set; }

        public int ArrivalAirportId { get; set; }

        public Airport ArrivalAirport { get; set; }

        public int AirlineId { get; set; }

        public Airline Airline { get; set; }

        public int AircraftId { get; set; }

        public Aircraft Aircraft { get; set; }

        public DateTime DepartureTime { get; set; }

        public DateTime ArrivalTime { get; set; }

        public TimeSpan FlightDuration => ArrivalTime - DepartureTime;

        public ClassType ClassType { get; set; }

        public SeatType SeatType { get; set; }

        public string SeatNumber { get; set; }

        public Reason Reason { get; set; }

        public int? DepartureAirportReviewId { get; set; }

        public AirportReview? DepartureAirportReview { get; set; }

        public int? ArrivalAirportReviewId { get; set; }

        public AirportReview? ArrivalAirportReview { get; set; }

        public int? AirlineReviewId { get; set; }

        public AirlineReview? AirlineReview { get; set; }

        public int? AircraftReviewId { get; set; }

        public AircraftReview? AircraftReview { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}

