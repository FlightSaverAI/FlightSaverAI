using FlightSaverApi.Commands.Post;
using FlightSaverApi.Data;
using MediatR;

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
        var post = await _context.SocialPosts.FindAsync(request.PostId, cancellationToken);

        if (post == null)
            throw new KeyNotFoundException("Post not found.");

        post.LikesCount++;
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}