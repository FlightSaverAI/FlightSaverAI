using FlightSaverApi.DTOs.Post;
using MediatR;

namespace FlightSaverApi.Commands.Post;

public class UpdatePostContentCommand : IRequest<SocialPostDTO>
{
    public int UserId { get; set; }
    public EditSocialPostContentDTO EditPostContentDto { get; set; }
}