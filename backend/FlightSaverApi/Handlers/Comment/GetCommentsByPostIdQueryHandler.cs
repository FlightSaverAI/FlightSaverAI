using AutoMapper;
using FlightSaverApi.Data;
using FlightSaverApi.DTOs.Post;
using FlightSaverApi.Queries.Comment;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers.Comment;

public class GetCommentsByPostIdQueryHandler : IRequestHandler<GetCommentsByPostIdQuery, IEnumerable<CommentDTO>>
{
    public readonly FlightSaverContext _context;
    public readonly IMapper _mapper;

    public GetCommentsByPostIdQueryHandler(FlightSaverContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;        
    }

    public async Task<IEnumerable<CommentDTO>> Handle(GetCommentsByPostIdQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Comments
            .Where(c => c.SocialPostId == request.PostId)
            .Include(p => p.User)
            .AsQueryable();
        
        var comments = await query.ToListAsync(cancellationToken);
        
        return _mapper.Map<IEnumerable<CommentDTO>>(comments);
    }
}