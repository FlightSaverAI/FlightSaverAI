using AutoMapper;
using FlightSaverApi.Commands.Comment;
using FlightSaverApi.Data;
using FlightSaverApi.DTOs.Post;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers.Comment;

public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, EditCommentDTO>
{
    private readonly FlightSaverContext _context;
    private readonly IMapper _mapper;

    public UpdateCommentCommandHandler(FlightSaverContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<EditCommentDTO> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
        
        if(comment == null)
        {
            throw new KeyNotFoundException($"Comment with Id {request.EditCommentDTO.Id} does not exist.");
        }

        if (comment.UserId != request.UserId)
        {
            throw new UnauthorizedAccessException("You are not authorized to edit this comment.");
        }
        
        comment.UpdatedAt = DateTime.UtcNow;

        _mapper.Map(request.EditCommentDTO, comment);
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return _mapper.Map<EditCommentDTO>(comment);
    }
}