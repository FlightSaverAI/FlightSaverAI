using AutoMapper;
using FlightSaverApi.DTOs.Aircraft;
using FlightSaverApi.DTOs.Flight;
using FlightSaverApi.Enums;
using FlightSaverApi.Enums.FlightEnums;
using FlightSaverApi.Models;
using FlightSaverApi.Models.Review;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FlightSaverApi.Mappings;

public class FlightProfile : Profile
{
    public FlightProfile()
    {
        CreateMap<FlightDTO, Flight>()
            .ForMember(dest => dest.AirlineReview, opt => opt.MapFrom(src => src.AirlineReview))
            .ForMember(dest => dest.AirportReviews, opt => opt.MapFrom(src => src.AirportReviews))
            .ForMember(dest => dest.AircraftReview, opt => opt.MapFrom(src => src.AircraftReview));
            
        CreateMap<Flight, FlightDTO>()
            .ForMember(dest => dest.AirlineReview, opt => opt.MapFrom(src => src.AirlineReview))
            .ForMember(dest => dest.AirportReviews, opt => opt.MapFrom(src => src.AirportReviews))
            .ForMember(dest => dest.AircraftReview, opt => opt.MapFrom(src => src.AircraftReview));

        CreateMap<Flight, MinimalFlightDTO>()
            .ForMember(dest => dest.DepartureAirportName, opt => opt.MapFrom(src => src.DepartureAirport.Name))
            .ForMember(dest => dest.DepartureAirportLatitude,
                opt => opt.MapFrom(src => src.DepartureAirport.Latitude))
            .ForMember(dest => dest.DepartureAirportLongitude,
                opt => opt.MapFrom(src => src.DepartureAirport.Longitude))
            .ForMember(dest => dest.ArrivalAirportName, opt => opt.MapFrom(src => src.ArrivalAirport.Name))
            .ForMember(dest => dest.ArrivalAirportLatitude, opt => opt.MapFrom(src => src.ArrivalAirport.Latitude))
            .ForMember(dest => dest.ArrivalAirportLongitude,
                opt => opt.MapFrom(src => src.ArrivalAirport.Longitude));
        
        CreateMap<Flight, NewFlightDTO>()
            .ForMember(dest => dest.flightDetailsForm, opt => opt.MapFrom(src => new FlightDetailsForm
            {
                departureAirportId = src.DepartureAirportId,
                arrivalAirportId = src.ArrivalAirportId,
                flightNumber = src.FlightNumber,
                departureDate = src.DepartureTime,
                departureTimeHour = src.DepartureTime.Hour,
                departureTimeMinutes = src.DepartureTime.Minute,
                arrivalTimeHour = src.ArrivalTime.Hour,
                arrivalTimeMinutes = src.ArrivalTime.Minute,
                flightDurationHour = src.FlightDuration.Hours,
                flightDurationMinutes = src.FlightDuration.Minutes
            }))
            
            .ForMember(dest => dest.airlineId, opt => opt.MapFrom(src => src.AirlineId))
            .ForMember(dest => dest.aircraftId, opt => opt.MapFrom(src => src.AircraftId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))

            .ForMember(dest => dest.ticketForm, opt => opt.MapFrom(src => new TicketForm
            {
                @class = src.ClassType.ToString(),
                seat = src.SeatType.ToString(),
                reason = src.Reason.ToString()
            }))
            
            .ForMember(dest => dest.rateAndReviewForm, opt => opt.MapFrom(src => new RateAndReviewForm
            {
                departureAirportOpinion = src.AirportReviews
                    .Where(r => r.AirportReviewType == AirportReviewType.Departure)
                    .Select(r => new DepartureAirportOpinion
                    {
                        rate = r.Rating,
                        review = r.Comment ?? string.Empty
                    })
                    .FirstOrDefault(),

                arrivalAirportOpinion = src.AirportReviews
                    .Where(r => r.AirportReviewType == AirportReviewType.Arrival)
                    .Select(r => new ArrivalAirportOpinion
                    {
                        rate = r.Rating,
                        review = r.Comment ?? string.Empty
                    })
                    .FirstOrDefault(),

                airlinesOpinion = src.AirlineReview == null ? null : new AirlineOpinion
                {
                    rate = src.AirlineReview.Rating,
                    review = src.AirlineReview.Comment ?? string.Empty
                },

                airPlaneOpinion = src.AircraftReview == null ? null : new AircraftOpinion
                {
                    rate = src.AircraftReview.Rating,
                    review = src.AircraftReview.Comment ?? string.Empty
                }
            }));

        CreateMap<NewFlightDTO, Flight>()
            .ForMember(dest => dest.FlightNumber, opt => opt.MapFrom(src => src.flightDetailsForm.flightNumber))
            .ForMember(dest => dest.DepartureAirportId,
                opt => opt.MapFrom(src => src.flightDetailsForm.departureAirportId))
            .ForMember(dest => dest.ArrivalAirportId, opt => opt.MapFrom(src => src.flightDetailsForm.arrivalAirportId))
            .ForMember(dest => dest.AirlineId, opt => opt.MapFrom(src => src.airlineId))
            .ForMember(dest => dest.AircraftId, opt => opt.MapFrom(src => src.aircraftId))
            .ForMember(dest => dest.DepartureTime, opt => opt.MapFrom(src =>
                new DateTime(
                    src.flightDetailsForm.departureDate.Year,
                    src.flightDetailsForm.departureDate.Month,
                    src.flightDetailsForm.departureDate.Day,
                    src.flightDetailsForm.departureTimeHour,
                    src.flightDetailsForm.departureTimeMinutes,
                    0).ToUniversalTime()))
            .ForMember(dest => dest.ArrivalTime, opt => opt.MapFrom(src =>
                new DateTime(
                    src.flightDetailsForm.departureDate.Year,
                    src.flightDetailsForm.departureDate.Month,
                    src.flightDetailsForm.departureDate.Day,
                    src.flightDetailsForm.arrivalTimeHour,
                    src.flightDetailsForm.arrivalTimeMinutes,
                    0).ToUniversalTime()))
            .ForMember(dest => dest.ClassType, opt => opt.MapFrom(src => Enum.Parse<ClassType>(src.ticketForm.@class)))
            .ForMember(dest => dest.SeatType, opt => opt.MapFrom(src => Enum.Parse<SeatType>(src.ticketForm.seat)))
            .ForMember(dest => dest.Reason, opt => opt.MapFrom(src => Enum.Parse<Reason>(src.ticketForm.reason)))
            .ForMember(dest => dest.SeatNumber, opt => opt.MapFrom(src => src.ticketForm.seatNumber))
            .ForMember(dest => dest.AirportReviews, opt => opt.MapFrom(src =>
                new List<AirportReview>
                {
                    new AirportReview
                    {
                        Rating = src.rateAndReviewForm.departureAirportOpinion.rate,
                        Comment = src.rateAndReviewForm.departureAirportOpinion.review,
                        AirportId = src.flightDetailsForm.departureAirportId,
                        FlightId = 0,
                        UserId = 0,
                        AirportReviewType = AirportReviewType.Departure
                    },
                    new AirportReview
                    {
                        Rating = src.rateAndReviewForm.arrivalAirportOpinion.rate,
                        Comment = src.rateAndReviewForm.arrivalAirportOpinion.review,
                        AirportId = src.flightDetailsForm.arrivalAirportId,
                        FlightId = 0,
                        UserId = 0,
                        AirportReviewType = AirportReviewType.Arrival
                    }
                }))
            .ForMember(dest => dest.AirlineReview, opt => opt.MapFrom(src =>
                new AirlineReview
                {
                    Rating = src.rateAndReviewForm.airlinesOpinion.rate,
                    Comment = src.rateAndReviewForm.airlinesOpinion.review,
                    AirlineId = src.airlineId,
                    FlightId = 0,
                    UserId = 0,
                }
            ))
            .ForMember(dest => dest.AircraftReview, opt => opt.MapFrom(src =>
                new AircraftReview()
                {
                    Rating = src.rateAndReviewForm.airPlaneOpinion.rate,
                    Comment = src.rateAndReviewForm.airPlaneOpinion.review,
                    AircraftId = src.aircraftId,
                    FlightId = 0,
                    UserId = 0,
                }
            ));
    }
}