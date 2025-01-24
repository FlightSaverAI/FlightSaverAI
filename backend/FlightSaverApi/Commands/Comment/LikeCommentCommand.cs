using MediatR;

namespace FlightSaverApi.Commands.Comment;

public class LikeCommentCommand : IRequest<Unit>
{
    public int CommentId { get; set; }
}