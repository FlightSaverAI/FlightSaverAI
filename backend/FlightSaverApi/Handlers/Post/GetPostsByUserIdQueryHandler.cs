using AutoMapper;
using FlightSaverApi.Data;
using FlightSaverApi.DTOs.Post;
using FlightSaverApi.Queries.Post;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers.Post;

public class GetPostsByUserIdQueryHandler : IRequestHandler<GetPostsByUserIdQuery, IEnumerable<SocialPostDTO>>
{
    public readonly FlightSaverContext _context;
    public readonly IMapper _mapper;

    public GetPostsByUserIdQueryHandler(FlightSaverContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;        
    }

    public async Task<IEnumerable<SocialPostDTO>> Handle(GetPostsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var posts = await _context.SocialPosts
            .Where(p => p.UserId == request.UserId)
            .Include(p => p.Comments)
            .Include(p => p.User)
            .ToListAsync(cancellationToken);
        
        return _mapper.Map<IEnumerable<SocialPostDTO>>(posts);
    }
}