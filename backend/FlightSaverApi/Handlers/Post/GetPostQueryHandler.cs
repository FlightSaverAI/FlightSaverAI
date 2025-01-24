using AutoMapper;
using FlightSaverApi.Data;
using FlightSaverApi.DTOs.Post;
using FlightSaverApi.Queries.Post;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers.Post;

public class GetPostQueryHandler : IRequestHandler<GetPostQuery, SocialPostDTO>
{
    private readonly FlightSaverContext _context;
    private readonly IMapper _mapper;

    public GetPostQueryHandler(FlightSaverContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SocialPostDTO> Handle(GetPostQuery request, CancellationToken cancellationToken)
    {
        var post = await _context.SocialPosts
            .Include(p => p.Comments)
            .Include(p => p.User)
            .Include(p => p.Likes)
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (post == null)
        {
            throw new KeyNotFoundException($"Post with Id {request.Id} does not exist.");
        }

        var postDto = _mapper.Map<SocialPostDTO>(post);

        // Check if the current user liked this post
        postDto.IsLikedByCurrentUser = post.Likes.Any(like => like.UserId == request.UserId);

        return postDto;
    }
}