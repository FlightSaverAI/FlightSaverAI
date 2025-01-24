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
            .Include(c => c.User)
            .Include(c => c.Likes);

        var comments = await query.ToListAsync(cancellationToken);

        var commentDtos = _mapper.Map<IEnumerable<CommentDTO>>(comments);

        foreach (var commentDto in commentDtos)
        {
            var commentEntity = comments.First(c => c.Id == commentDto.Id);
            commentDto.IsLikedByCurrentUser = commentEntity.Likes.Any(like => like.UserId == request.UserId);
        }

        return commentDtos;
    }
}