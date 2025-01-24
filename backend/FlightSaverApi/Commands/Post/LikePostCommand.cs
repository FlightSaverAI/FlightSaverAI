using MediatR;

namespace FlightSaverApi.Commands.Post;

public class LikePostCommand : IRequest<Unit>
{
    public int PostId { get; set; }
    public int UserId { get; set; }
}