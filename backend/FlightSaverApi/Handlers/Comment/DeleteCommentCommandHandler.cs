using AutoMapper;
using FlightSaverApi.Commands.Comment;
using FlightSaverApi.Data;
using MediatR;

namespace FlightSaverApi.Handlers.Comment;

public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, Unit>
{
    public readonly FlightSaverContext _context;
    public readonly IMapper _mapper;

    public DeleteCommentCommandHandler(FlightSaverContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _context.Comments.FindAsync(request.Id, cancellationToken);
        if (comment == null)
        {
            throw new KeyNotFoundException($"Comment with Id {request.Id} does not exist.");
        }
        
        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}