using FlightSaverApi.DTOs.Post;
using MediatR;

namespace FlightSaverApi.Commands.Post;

public class UpdatePostImageCommand : IRequest<SocialPostDTO>
{
    public int UserId { get; set; }
    public EditSocialPostImageDTO EditPostImageDTO { get; set; }
}