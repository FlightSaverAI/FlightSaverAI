using FlightSaverApi.Commands.Post;
using FlightSaverApi.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers.Post;

public class UnlikePostCommandHandler : IRequestHandler<UnlikePostCommand, Unit>
{
    private readonly FlightSaverContext _context;

    public UnlikePostCommandHandler(FlightSaverContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UnlikePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _context.SocialPosts.FindAsync(new object[] { request.PostId }, cancellationToken);

        if (post == null)
            throw new KeyNotFoundException("Post not found.");

        // Check if the user has liked the post
        var like = await _context.PostLikes
            .FirstOrDefaultAsync(pl => pl.PostId == request.PostId && pl.UserId == request.UserId, cancellationToken);

        if (like == null)
            throw new InvalidOperationException("You have not liked this post.");

        // Remove the like
        _context.PostLikes.Remove(like);

        post.LikesCount--;
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}