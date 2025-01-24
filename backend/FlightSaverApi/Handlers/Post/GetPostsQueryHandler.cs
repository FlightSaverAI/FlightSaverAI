using AutoMapper;
using FlightSaverApi.Data;
using FlightSaverApi.DTOs.Post;
using FlightSaverApi.Queries.Post;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers.Post;

public class GetPostsQueryHandler : IRequestHandler<GetPostsQuery, IEnumerable<SocialPostDTO>>
{
    public readonly FlightSaverContext _context;
    public readonly IMapper _mapper;

    public GetPostsQueryHandler(FlightSaverContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;        
    }

    public async Task<IEnumerable<SocialPostDTO>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
    {
        var posts = await _context.SocialPosts
            .Include(p => p.Comments)
            .Include(p => p.User)
            .ToListAsync(cancellationToken);
        
        return _mapper.Map<IEnumerable<SocialPostDTO>>(posts);
    }
}