using FlightSaverApi.Commands.Comment;
using FlightSaverApi.Data;
using FlightSaverApi.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers.Comment;

public class LikeCommentCommandHandler : IRequestHandler<LikeCommentCommand, Unit>
{
    private readonly FlightSaverContext _context;

    public LikeCommentCommandHandler(FlightSaverContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(LikeCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _context.Comments.FindAsync(new object[] { request.CommentId }, cancellationToken);

        if (comment == null)
            throw new KeyNotFoundException("Comment not found.");

        // Check if the user already liked the comment
        var alreadyLiked = await _context.CommentLikes
            .AnyAsync(cl => cl.CommentId == request.CommentId && cl.UserId == request.UserId, cancellationToken);

        if (alreadyLiked)
            throw new InvalidOperationException("You have already liked this comment.");

        // Add the like
        _context.CommentLikes.Add(new CommentLike
        {
            CommentId = request.CommentId,
            UserId = request.UserId
        });

        comment.LikesCount++;
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}