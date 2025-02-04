using FlightSaverApi.DTOs.Post;
using MediatR;
using Newtonsoft.Json;

namespace FlightSaverApi.Commands.Post;

public class CreatePostCommand : IRequest<SocialPostDTO>
{
    public int UserId { get; set; }
    public NewPostDTO Post { get; set; }
}