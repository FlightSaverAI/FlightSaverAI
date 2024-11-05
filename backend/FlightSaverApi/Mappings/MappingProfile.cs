using System;
using AutoMapper;
using FlightSaverApi.Enums;
using FlightSaverApi.Models.AircraftModel;
using FlightSaverApi.Models.AirlineModel;
using FlightSaverApi.Models.AirportModel;
using FlightSaverApi.Models.FlightModel;
using FlightSaverApi.Models.ReviewModel;
using FlightSaverApi.Models.UserModel;

namespace FlightSaverApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Aircraft, AircraftDTO>().ReverseMap();

            CreateMap<User, UserLoginDTO>();

            CreateMap<UserRegisterDTO, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => UserRole.User));

            CreateMap<Airline, AirlineDTO>().ReverseMap();

            CreateMap<Airport, AirportDTO>().ReverseMap();

            CreateMap<Flight, FlightDTO>().ReverseMap();

            CreateMap<Review, ReviewDTO>()
                .Include<AircraftReview, AircraftReviewDTO>()
                .Include<AirlineReview, AirlineReviewDTO>()
                .Include<AirportReview, AirportReviewDTO>();

            CreateMap<ReviewDTO, Review>()
                .Include<AircraftReviewDTO, AircraftReview>()
                .Include<AirlineReviewDTO, AirlineReview>()
                .Include<AirportReviewDTO, AirportReview>();

            CreateMap<AircraftReview, AircraftReviewDTO>().ReverseMap();

            CreateMap<AirlineReview, AirlineReviewDTO>().ReverseMap();

            CreateMap<AirportReview, AirportReviewDTO>().ReverseMap();
        }
    }
}
