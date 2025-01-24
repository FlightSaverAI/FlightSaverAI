using FlightSaverApi.Commands.Comment;
using FlightSaverApi.Data;
using MediatR;

namespace FlightSaverApi.Handlers.Comment;

public class UnlikeCommentCommandHandler : IRequestHandler<UnlikeCommentCommand, Unit>
{
    private readonly FlightSaverContext _context;

    public UnlikeCommentCommandHandler(FlightSaverContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UnlikeCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _context.Comments.FindAsync(new object[] { request.CommentId }, cancellationToken);

        if (comment == null)
            throw new KeyNotFoundException("Comment not found.");

        if (comment.LikesCount > 0)
        {
            comment.LikesCount--;
            await _context.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}