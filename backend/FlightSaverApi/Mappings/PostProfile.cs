using AutoMapper;
using FlightSaverApi.DTOs.Post;
using FlightSaverApi.Models;

namespace FlightSaverApi.Mappings;

public class PostProfile : Profile
{
    public PostProfile()
    {
        CreateMap<SocialPost, SocialPostDTO>()
            .ForMember(dest => dest.UserId,
                opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.CommentsCount, opt => opt.MapFrom(src => src.Comments != null ? src.Comments.Count : 0));

        CreateMap<SocialPostDTO, SocialPost>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Comments, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.PostedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.LikesCount, opt => opt.Ignore())
            .ForMember(dest => dest.CommentsCount, opt => opt.Ignore());

        CreateMap<Comment, CommentDTO>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));

        CreateMap<CommentDTO, Comment>()
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.SocialPost, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore());

        CreateMap<EditSocialPostDTO, SocialPost>()
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.location))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.content))
            .ForAllMembers(opt =>
                opt.Condition((src, dest, srcMember) =>
                    srcMember != null && !(srcMember is string str && string.IsNullOrEmpty(str))));

        CreateMap<SocialPost, EditSocialPostDTO>()
            .ForMember(dest => dest.location, opt => opt.MapFrom(src => src.Location))
            .ForMember(dest => dest.content, opt => opt.MapFrom(src => src.Content));

        CreateMap<EditCommentDTO, Comment>()
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.content))
            .ForAllMembers(opt =>
                opt.Condition((src, dest, srcMember) =>
                    srcMember != null && !(srcMember is string str && string.IsNullOrEmpty(str))));
        
        CreateMap<Comment, EditCommentDTO>()
            .ForMember(dest => dest.content, opt => opt.MapFrom(src => src.Content));
        
        CreateMap<NewCommentDTO, Comment>()
            .ForMember(dest => dest.SocialPostId, opt => opt.MapFrom(src => src.postId))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.content))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.PostedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.LikesCount, opt => opt.Ignore())
            .ForMember(dest => dest.SocialPost, opt => opt.Ignore());

        CreateMap<Comment, NewCommentDTO>()
            .ForMember(dest => dest.postId, opt => opt.MapFrom(src => src.SocialPostId))
            .ForMember(dest => dest.content, opt => opt.MapFrom(src => src.Content));

        CreateMap<NewPostDTO, SocialPost>()
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.location))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.content))
            .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.PostedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.LikesCount, opt => opt.Ignore())
            .ForMember(dest => dest.CommentsCount, opt => opt.Ignore())
            .ForMember(dest => dest.Comments, opt => opt.Ignore());

        CreateMap<SocialPost, NewPostDTO>()
            .ForMember(dest => dest.location, opt => opt.MapFrom(src => src.Location))
            .ForMember(dest => dest.content, opt => opt.MapFrom(src => src.Content));
    }
}