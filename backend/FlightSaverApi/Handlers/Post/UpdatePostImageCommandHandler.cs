using AutoMapper;
using FlightSaverApi.Commands.Post;
using FlightSaverApi.Data;
using FlightSaverApi.DTOs.Post;
using FlightSaverApi.Interfaces.Services;
using FlightSaverApi.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers.Post;

public class UpdatePostImageCommandHandler : IRequestHandler<UpdatePostImageCommand, SocialPostDTO>
{
    private readonly FlightSaverContext _context;
    private readonly IBlobStorageService _blobStorageService;
    private readonly IMapper _mapper;

    public UpdatePostImageCommandHandler(FlightSaverContext context, IBlobStorageService blobStorageService, IMapper mapper)
    {
        _context = context;
        _blobStorageService = blobStorageService;
        _mapper = mapper;
    }

    public async Task<SocialPostDTO> Handle(UpdatePostImageCommand request, CancellationToken cancellationToken)
    {
        var post = await _context.SocialPosts.FirstOrDefaultAsync(c => c.Id == request.EditPostImageDTO.id, cancellationToken);
        if (post == null)
        {
            throw new KeyNotFoundException($"Post with Id {request.EditPostImageDTO.id} does not exist.");
        }
        if (post.UserId != request.UserId)
        {
            throw new UnauthorizedAccessException("You are not authorized to edit this post.");
        }

        if (request.EditPostImageDTO.image == null)
        {
            post.ImageUrl = "";
        }
        else if (request.EditPostImageDTO.image != null)
        {
            var imageUrl = await _blobStorageService.UploadImageAsync(request.EditPostImageDTO.image);
            var imageRecord = new ImageRecord { Url = imageUrl };

            _context.Images.Add(imageRecord);
            await _context.SaveChangesAsync(cancellationToken);

            post.ImageUrl = imageUrl;
        }

        post.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<SocialPostDTO>(post);
    }
}