using AutoMapper;
using FlightSaverApi.Commands.Post;
using FlightSaverApi.Data;
using FlightSaverApi.DTOs.Post;
using FlightSaverApi.Models;
using MediatR;

namespace FlightSaverApi.Handlers.Post;

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, NewPostDTO>
{
    private readonly FlightSaverContext _context;
    private readonly IMapper _mapper;

    public CreatePostCommandHandler(FlightSaverContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<NewPostDTO> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var post = _mapper.Map<SocialPost>(request.Post);

        post.UserId = request.Post.UserId.Value;
        post.PostedAt = DateTime.UtcNow;
        post.LikesCount = 0;
        post.CommentsCount = 0;
        
        _context.SocialPosts.Add(post);
        await _context.SaveChangesAsync(cancellationToken);
        
        return _mapper.Map<NewPostDTO>(post);
    }
}