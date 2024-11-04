using System;
using System.ComponentModel.DataAnnotations;
using FlightSaverApi.Enums.FlightEnums;
using FlightSaverApi.Models.ReviewModel;

namespace FlightSaverApi.Models.FlightModel
{
    public class FlightDTO
    {
        public int Id { get; set; }

        [Required]
        public required string FlightNumber { get; set; }

        [Required]
        public required int DepartureAirportId { get; set; }

        [Required]
        public required int ArrivalAirportId { get; set; }

        [Required]
        public required int? AirlineId { get; set; }

        [Required]
        public required int? AircraftId { get; set; }

        [Required]
        public required DateTime DepartureTime { get; set; }

        [Required]
        public required DateTime ArrivalTime { get; set; }

        [Required]
        public required ClassType ClassType { get; set; }

        [Required]
        public required SeatType SeatType { get; set; }

        [Required]
        public required string? SeatNumber { get; set; }

        [Required]
        public required Reason Reason { get; set; }

        [Required]
        public required int UserId { get; set; }

        public AirportReviewDTO? DepartureAirportReview { get; set; }

        public AirportReviewDTO? ArrivalAirportReview { get; set; }

        public AirlineReviewDTO? AirlineReview { get; set; }

        public AircraftReviewDTO? AircraftReview { get; set; }
    }
}

