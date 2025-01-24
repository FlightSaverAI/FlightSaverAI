using FlightSaverApi.Commands.Post;
using FlightSaverApi.Data;
using FlightSaverApi.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers.Post;

public class LikePostCommandHandler : IRequestHandler<LikePostCommand, Unit>
{
    private readonly FlightSaverContext _context;

    public LikePostCommandHandler(FlightSaverContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(LikePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _context.SocialPosts.FindAsync(new object[] { request.PostId }, cancellationToken);

        if (post == null)
            throw new KeyNotFoundException("Post not found.");

        // Check if the user already liked the post
        var alreadyLiked = await _context.PostLikes
            .AnyAsync(pl => pl.PostId == request.PostId && pl.UserId == request.UserId, cancellationToken);

        if (alreadyLiked)
            throw new InvalidOperationException("You have already liked this post.");

        // Add the like
        _context.PostLikes.Add(new PostLike
        {
            PostId = request.PostId,
            UserId = request.UserId
        });

        post.LikesCount++;
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}