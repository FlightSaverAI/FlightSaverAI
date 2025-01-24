using FlightSaverApi.Commands.Comment;
using FlightSaverApi.Data;
using MediatR;

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

        comment.LikesCount++;
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}