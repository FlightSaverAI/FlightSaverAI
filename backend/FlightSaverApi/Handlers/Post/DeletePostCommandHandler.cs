using AutoMapper;
using FlightSaverApi.Commands.Post;
using FlightSaverApi.Data;
using FlightSaverApi.DTOs.Post;
using MediatR;

namespace FlightSaverApi.Handlers.Post;

public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, Unit>
{
    public readonly FlightSaverContext _context;
    public readonly IMapper _mapper;

    public DeletePostCommandHandler(FlightSaverContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _context.SocialPosts.FindAsync(request.Id, cancellationToken);
        if (post == null)
        {
            throw new KeyNotFoundException($"Post with Id {request.Id} does not exist.");
        }

        if (post.UserId != request.UserId)
        {
            throw new UnauthorizedAccessException("You are not authorized to delete this post.");
        }
        
        _context.SocialPosts.Remove(post);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}