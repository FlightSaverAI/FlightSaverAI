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
        var friendIds = await _context.Users
            .Where(u => u.Id == request.UserId)
            .SelectMany(u => u.Friends.Select(f => f.Id))
            .ToListAsync(cancellationToken);
        
        friendIds.Add(request.UserId);

        var query = _context.SocialPosts
            .Include(p => p.User)
            .Include(p => p.Comments)
            .Include(p => p.Likes)
            .Where(p => friendIds.Contains(p.UserId));

        if (request.LastPostId.HasValue)
        {
            var lastPost = await _context.SocialPosts.FindAsync(request.LastPostId);
            if (lastPost != null)
            {
                query = query.Where(p => p.PostedAt < lastPost.PostedAt);
            }
        }

        var posts = await query
            .OrderByDescending(p => p.PostedAt)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var postDtos = _mapper.Map<IEnumerable<SocialPostDTO>>(posts);

        foreach (var postDto in postDtos)
        {
            var postEntity = posts.First(p => p.Id == postDto.Id);
            postDto.IsLikedByCurrentUser = postEntity.Likes.Any(like => like.UserId == request.UserId);
        }

        return postDtos;
    }
}