using MediatR;

namespace FlightSaverApi.Commands.Comment;

public class UnlikeCommentCommand : IRequest<Unit>
{
    public int CommentId { get; set; }
}