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
                .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<AirportDTO, Airport>()
                .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<AircraftDTO, Aircraft>()
                .ReverseMap();

            CreateMap<FlightDTO, Flight>()
               .ForMember(dest => dest.DepartureAirportReview, opt => opt.MapFrom(src => src.DepartureAirportReview))
               .ForMember(dest => dest.ArrivalAirportReview, opt => opt.MapFrom(src => src.ArrivalAirportReview))
               .ForMember(dest => dest.AirlineReview, opt => opt.MapFrom(src => src.AirlineReview))
               .ForMember(dest => dest.AircraftReview, opt => opt.MapFrom(src => src.AircraftReview));

            CreateMap<AirportReview, AirportReviewDTO>();

            CreateMap<AirlineReview, AirlineReviewDTO>();

            CreateMap<AircraftReview, AircraftReviewDTO>();
        }
    }
}
