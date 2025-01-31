using AutoMapper;
using FlightSaverApi.DTOs.User;
using FlightSaverApi.Enums;
using FlightSaverApi.Models;

namespace FlightSaverApi.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
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

        CreateMap<User, UserInfoDTO>()
            .ForMember(dest => dest.Password, opt => opt.Ignore());

        CreateMap<User, EditUserDTO>()
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ForMember(dest => dest.ProfilePictureImage, opt => opt.Ignore())
            .ForMember(dest => dest.BackgroundPictureImage, opt => opt.Ignore());

        CreateMap<EditUserDTO, User>()
            .ForMember(dest => dest.AirportReviews, opt => opt.Ignore())
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore())
            .ForMember(dest => dest.Role, opt => opt.Ignore())
            .ForMember(dest => dest.Comments, opt => opt.Ignore())
            .ForMember(dest => dest.Flights, opt => opt.Ignore())
            .ForMember(dest => dest.AirlineReviews, opt => opt.Ignore())
            .ForMember(dest => dest.SocialPosts, opt => opt.Ignore())
            .ForAllMembers(opt =>
                opt.Condition((src, dest, srcMember) =>
                    srcMember != null && !(srcMember is string str && string.IsNullOrEmpty(str))));
        
        CreateMap<User, FriendDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.ProfilePictureUrl, opt => opt.MapFrom(src => src.ProfilePictureUrl))
            .ForMember(dest => dest.BackgroundPictureUrl, opt => opt.MapFrom(src => src.BackgroundPictureUrl))
            .ForMember(dest => dest.Statistics, opt => opt.Ignore()) // Customize how to map Statistics if needed
            .ForAllMembers(opt =>
                opt.Condition((src, dest, srcMember) =>
                    srcMember != null && !(srcMember is string str && string.IsNullOrEmpty(str))));
    }
}