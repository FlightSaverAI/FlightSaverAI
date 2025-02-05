using AutoMapper;
using FlightSaverApi.Commands.Post;
using FlightSaverApi.Data;
using FlightSaverApi.DTOs.Post;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers.Post;

public class UpdatePostContentCommandHandler: IRequestHandler<UpdatePostContentCommand, SocialPostDTO>
{
    private readonly FlightSaverContext _context;
    private readonly IMapper _mapper;

    public UpdatePostContentCommandHandler(FlightSaverContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SocialPostDTO> Handle(UpdatePostContentCommand request, CancellationToken cancellationToken)
    {
        var post = await _context.SocialPosts.FirstOrDefaultAsync(c => c.Id == request.EditPostContentDto.id, cancellationToken);
        if (post == null)
        {
            throw new KeyNotFoundException($"Post with Id {request.EditPostContentDto.id} does not exist.");
        }
        if (post.UserId != request.UserId)
        {
            throw new UnauthorizedAccessException("You are not authorized to edit this post.");
        }
        
        post.UpdatedAt = DateTime.UtcNow;
        _mapper.Map(request.EditPostContentDto, post);

        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<SocialPostDTO>(post);
    }
}