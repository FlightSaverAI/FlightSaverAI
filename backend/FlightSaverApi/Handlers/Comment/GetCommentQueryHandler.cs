using AutoMapper;
using FlightSaverApi.Data;
using FlightSaverApi.DTOs.Post;
using FlightSaverApi.Queries.Comment;
using MediatR;

namespace FlightSaverApi.Handlers.Comment;

public class GetCommentQueryHandler : IRequestHandler<GetCommentQuery, CommentDTO>
{
    private readonly FlightSaverContext _context;
    private readonly IMapper _mapper;

    public GetCommentQueryHandler(FlightSaverContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CommentDTO> Handle(GetCommentQuery request, CancellationToken cancellationToken)
    {
        var comment = await _context.Comments.FindAsync(request.Id, cancellationToken);
        
        if(comment == null)
        {
            throw new KeyNotFoundException($"Comment with Id {request.Id} does not exist.");
        }
        
        var commentDto = _mapper.Map<CommentDTO>(comment);
        
        return commentDto;
    }
}