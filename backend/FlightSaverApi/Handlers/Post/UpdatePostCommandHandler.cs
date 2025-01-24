using AutoMapper;
using FlightSaverApi.Commands.Comment;
using FlightSaverApi.Commands.Post;
using FlightSaverApi.Data;
using FlightSaverApi.DTOs.Post;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers.Post;

public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, EditSocialPostDTO>
{
    private readonly FlightSaverContext _context;
    private readonly IMapper _mapper;

    public UpdatePostCommandHandler(FlightSaverContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<EditSocialPostDTO> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        var posts = await _context.SocialPosts.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
        
        if(posts == null)
        {
            throw new KeyNotFoundException($"Post with Id {request.EditSocialPostDTO.Id} does not exist.");
        }

        if (posts.UserId != request.UserId)
        {
            throw new UnauthorizedAccessException("You are not authorized to edit this post.");
        }
        
        posts.UpdatedAt = DateTime.UtcNow;

        _mapper.Map(request.EditSocialPostDTO, posts);
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return _mapper.Map<EditSocialPostDTO>(posts);
    }
}