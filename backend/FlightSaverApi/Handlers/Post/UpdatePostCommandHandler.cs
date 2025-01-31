using AutoMapper;
using FlightSaverApi.Commands.Comment;
using FlightSaverApi.Commands.Post;
using FlightSaverApi.Data;
using FlightSaverApi.DTOs.Post;
using FlightSaverApi.Interfaces.Services;
using FlightSaverApi.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers.Post;

public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, EditSocialPostDTO>
{
    private readonly FlightSaverContext _context;
    private readonly IMapper _mapper;
    private readonly IBlobStorageService _blobStorageService;

    public UpdatePostCommandHandler(FlightSaverContext context, IMapper mapper, IBlobStorageService blobStorageService)
    {
        _context = context;
        _mapper = mapper;
        _blobStorageService = blobStorageService;
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
        
        if (request.EditSocialPostDTO.Image is not null)
        {
            var imageUrl = await _blobStorageService.UploadImageAsync(request.EditSocialPostDTO.Image);

            var imageRecord = new ImageRecord()
            {
                Url = imageUrl
            };
            
            _context.Images.Add(imageRecord);
            await _context.SaveChangesAsync(cancellationToken);
            
            posts.ImageUrl = imageUrl;
        }
        
        posts.UpdatedAt = DateTime.UtcNow;

        _mapper.Map(request.EditSocialPostDTO, posts);
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return _mapper.Map<EditSocialPostDTO>(posts);
    }
}