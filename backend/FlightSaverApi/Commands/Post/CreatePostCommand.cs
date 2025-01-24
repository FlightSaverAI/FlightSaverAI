using FlightSaverApi.DTOs.Post;
using MediatR;
using Newtonsoft.Json;

namespace FlightSaverApi.Commands.Post;

public class CreatePostCommand : IRequest<NewPostDTO>
{
    public NewPostDTO Post { get; set; }
}