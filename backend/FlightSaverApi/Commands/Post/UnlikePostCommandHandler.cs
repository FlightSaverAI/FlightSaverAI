using FlightSaverApi.Data;
using MediatR;

namespace FlightSaverApi.Commands.Post;

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

        if (post.LikesCount > 0)
        {
            post.LikesCount--;
            await _context.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}