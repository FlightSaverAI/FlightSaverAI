using AutoMapper;
using FlightSaverApi.DTOs.Post;
using FlightSaverApi.Models;

namespace FlightSaverApi.Mappings;

public class PostProfile : Profile
{
    public PostProfile()
    {
        CreateMap<SocialPost, SocialPostDTO>()
            .ForMember(dest => dest.UserName,
                opt => opt.MapFrom(src => src.User.Username))
            .ForMember(dest => dest.CommentsCount, opt => opt.MapFrom(src => src.Comments.Count));

        CreateMap<SocialPostDTO, SocialPost>()
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.Comments, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<Comment, CommentDTO>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Username))
            .ForMember(dest => dest.SocialPostId, opt => opt.MapFrom(src => src.SocialPost.Id));

        CreateMap<CommentDTO, Comment>()
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.SocialPost, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}