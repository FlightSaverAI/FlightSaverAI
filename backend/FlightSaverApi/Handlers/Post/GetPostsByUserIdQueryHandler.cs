using AutoMapper;
using FlightSaverApi.Data;
using FlightSaverApi.DTOs.Post;
using FlightSaverApi.Queries.Post;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers.Post;

public class GetPostsByUserIdQueryHandler : IRequestHandler<GetPostsByUserIdQuery, IEnumerable<SocialPostDTO>>
{
    private readonly FlightSaverContext _context;
    private readonly IMapper _mapper;

    public GetPostsByUserIdQueryHandler(FlightSaverContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SocialPostDTO>> Handle(GetPostsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var query = _context.SocialPosts
            .Include(p => p.Likes)
            .Where(p => p.UserId == request.UserId);

        if (request.LastPostId.HasValue)
        {
            var lastPost = await _context.SocialPosts.FindAsync(request.LastPostId);
            if (lastPost != null)
            {
                query = query.Where(p => p.PostedAt < lastPost.PostedAt);
            }
        }

        query = query.OrderByDescending(p => p.PostedAt);

        var posts = await query
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