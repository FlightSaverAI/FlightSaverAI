using AutoMapper;
using FlightSaverApi.Commands.Comment;
using FlightSaverApi.Data;
using FlightSaverApi.DTOs.Post;
using MediatR;

namespace FlightSaverApi.Handlers.Comment;

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, NewCommentDTO>
{
    private readonly FlightSaverContext _context;
    private readonly IMapper _mapper;

    public CreateCommentCommandHandler(FlightSaverContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<NewCommentDTO> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = _mapper.Map<Models.Comment>(request.Comment);

        comment.UserId = request.Comment.UserId.Value;
        comment.SocialPostId = request.Comment.PostId;
        comment.PostedAt = DateTime.UtcNow;
        comment.LikesCount = 0;

        _context.Comments.Add(comment);
        await _context.SaveChangesAsync(cancellationToken);
        
        return _mapper.Map<NewCommentDTO>(comment);
    }
}