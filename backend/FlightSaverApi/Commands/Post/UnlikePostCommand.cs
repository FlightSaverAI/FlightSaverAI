using MediatR;

namespace FlightSaverApi.Commands.Post;

public class UnlikePostCommand : IRequest<Unit>
{
    public int PostId { get; set; }
}