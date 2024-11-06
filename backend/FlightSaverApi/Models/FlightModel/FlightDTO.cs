using System;
using System.ComponentModel.DataAnnotations;
using FlightSaverApi.Enums.FlightEnums;
using FlightSaverApi.Models.ReviewModel;

namespace FlightSaverApi.Models.FlightModel
{
    public class FlightDTO
    {
        public int Id { get; set; }

        public string FlightNumber { get; set; }

        public int DepartureAirportId { get; set; }

        public int ArrivalAirportId { get; set; }

        public int AirlineId { get; set; }

        public int AircraftId { get; set; }

        public DateTime DepartureTime { get; set; }

        public DateTime ArrivalTime { get; set; }

        public ClassType ClassType { get; set; }

        public SeatType SeatType { get; set; }

        public string SeatNumber { get; set; }

        public Reason Reason { get; set; }

        public int UserId { get; set; }

        public AirportReviewDTO? DepartureAirportReview { get; set; }

        public AirportReviewDTO? ArrivalAirportReview { get; set; }

        public AirlineReviewDTO? AirlineReview { get; set; }

        public AircraftReviewDTO? AircraftReview { get; set; }
    }
}

