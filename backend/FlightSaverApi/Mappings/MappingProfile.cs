using System;
using AutoMapper;
using FlightSaverApi.DTOs;
using FlightSaverApi.DTOs.Flight;
using FlightSaverApi.DTOs.Review;
using FlightSaverApi.DTOs.User;
using FlightSaverApi.Enums;
using FlightSaverApi.Models;
using FlightSaverApi.Models.ReviewModel;

namespace FlightSaverApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegisterDTO, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => UserRole.User));

            CreateMap<UserLoginDTO, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Username, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore());

            CreateMap<User, UserRegisterDTO>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());

            CreateMap<AirlineDTO, Airline>()
                .ReverseMap();

            CreateMap<AirportDTO, Airport>()
                .ReverseMap();

            CreateMap<AircraftDTO, Aircraft>()
                .ReverseMap();
                

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
                
            CreateMap<AirportReview, AirportReviewDTO>()
                .ReverseMap();

            CreateMap<AirlineReview, AirlineReviewDTO>()
                .ReverseMap();

            CreateMap<AircraftReview, AircraftReviewDTO>()
                .ReverseMap();
        }
    }
}
