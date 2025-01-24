using AutoMapper;
using FlightSaverApi.DTOs.Review;
using FlightSaverApi.Models.Review;

namespace FlightSaverApi.Mappings;

public class ReviewProfile : Profile
{
    public ReviewProfile()
    {
        CreateMap<AirportReview, AirportReviewDTO>()
            .ReverseMap();

        CreateMap<AirlineReview, AirlineReviewDTO>()
            .ReverseMap();

        CreateMap<AircraftReview, AircraftReviewDTO>()
            .ReverseMap();
    }
}