using AutoMapper;
using FlightSaverApi.Data;
using FlightSaverApi.DTOs.Post;
using FlightSaverApi.Queries.Comment;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers.Comment;

public class GetCommentsQueryHandler : IRequestHandler<GetCommentsQuery, IEnumerable<CommentDTO>>
{
    public readonly FlightSaverContext _context;
    public readonly IMapper _mapper;

    public GetCommentsQueryHandler(FlightSaverContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;        
    }

    public async Task<IEnumerable<CommentDTO>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
    {
        var comments = await _context.Comments.ToListAsync(cancellationToken);
        
        return _mapper.Map<IEnumerable<CommentDTO>>(comments);
    }
}