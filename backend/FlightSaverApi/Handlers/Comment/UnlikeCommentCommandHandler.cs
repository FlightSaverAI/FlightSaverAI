using FlightSaverApi.Commands.Comment;
using FlightSaverApi.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

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

        // Check if the user has liked the comment
        var like = await _context.CommentLikes
            .FirstOrDefaultAsync(cl => cl.CommentId == request.CommentId && cl.UserId == request.UserId, cancellationToken);

        if (like == null)
            throw new InvalidOperationException("You have not liked this comment.");

        // Remove the like
        _context.CommentLikes.Remove(like);

        comment.LikesCount--;
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}