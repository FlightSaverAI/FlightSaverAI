using AutoMapper;
using FlightSaverApi.Commands.Post;
using FlightSaverApi.Data;
using FlightSaverApi.DTOs.Post;
using FlightSaverApi.Interfaces.Services;
using FlightSaverApi.Models;
using MediatR;

namespace FlightSaverApi.Handlers.Post;

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, NewPostDTO>
{
    private readonly FlightSaverContext _context;
    private readonly IMapper _mapper;
    private readonly IBlobStorageService _blobStorageService;

    public CreatePostCommandHandler(FlightSaverContext context, IMapper mapper, IBlobStorageService blobStorageService)
    {
        _context = context;
        _mapper = mapper;
        _blobStorageService = blobStorageService;
    }

    public async Task<NewPostDTO> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var post = _mapper.Map<SocialPost>(request.Post);

        post.UserId = request.Post.UserId.Value;
        post.PostedAt = DateTime.UtcNow;
        post.LikesCount = 0;
        post.CommentsCount = 0;

        if (request.Post.Image is not null)
        {
            var imageUrl = await _blobStorageService.UploadImageAsync(request.Post.Image);

            var imageRecord = new ImageRecord()
            {
                Url = imageUrl
            };
            
            _context.Images.Add(imageRecord);
            await _context.SaveChangesAsync(cancellationToken);
            
            post.ImageUrl = imageUrl;
        }
        
        _context.SocialPosts.Add(post);
        await _context.SaveChangesAsync(cancellationToken);
        
        return _mapper.Map<NewPostDTO>(post);
    }
}