using AutoMapper;
using FlightSaverApi.Data;
using FlightSaverApi.DTOs.Post;
using FlightSaverApi.Queries.Post;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers.Post;

public class GetFriendsPostsQueryHandler : IRequestHandler<GetFriendsPostsQuery, IEnumerable<SocialPostDTO>>
{
    private readonly FlightSaverContext _context;
    private readonly IMapper _mapper;

    public GetFriendsPostsQueryHandler(FlightSaverContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SocialPostDTO>> Handle(GetFriendsPostsQuery request, CancellationToken cancellationToken)
    {
        // Fetch friends of the user
        var friends = await _context.Users
            .Include(u => u.Friends)
            .Where(u => u.Id == request.UserId)
            .SelectMany(u => u.Friends)
            .ToListAsync(cancellationToken);

        var friendIds = friends.Select(f => f.Id).ToList();

        // Fetch posts by friends
        var posts = await _context.SocialPosts
            .Include(p => p.User)
            .Include(p => p.Comments)
            .Where(p => friendIds.Contains(p.UserId))
            .ToListAsync(cancellationToken);

        return _mapper.Map<IEnumerable<SocialPostDTO>>(posts);
    }
}