using AutoMapper;
using FlightSaverApi.Commands.User;
using FlightSaverApi.Data;
using FlightSaverApi.DTOs.User;
using FlightSaverApi.Interfaces.Services;
using FlightSaverApi.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers.User;

public class UpdatePicturesCommandHandler : IRequestHandler<UpdatePicturesCommand, EditedUserDTO>
    {
        private readonly FlightSaverContext _context;
        private readonly IMapper _mapper;
        private readonly IBlobStorageService _blobStorageService;

        public UpdatePicturesCommandHandler(FlightSaverContext context, IMapper mapper, IBlobStorageService blobStorageService)
        {
            _context = context;
            _mapper = mapper;
            _blobStorageService = blobStorageService;
        }
        
        public async Task<EditedUserDTO> Handle(UpdatePicturesCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(a => a.Id == request.UserId, cancellationToken);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with Id {request.UserId} does not exist.");
            }

            if (request.UpdatePicturesDto.ProfilePictureImage == null)
            {
                user.ProfilePictureUrl = null;
            }

            if (request.UpdatePicturesDto.BackgroundPictureImage == null)
            {
                user.BackgroundPictureUrl = null;
            }
            
            if (request.UpdatePicturesDto.ProfilePictureImage != null)
            {
                var profileUrl = await _blobStorageService.UploadImageAsync(request.UpdatePicturesDto.ProfilePictureImage);
                // Optionally, save a record for the image
                var imageRecord = new ImageRecord { Url = profileUrl };
                _context.Images.Add(imageRecord);
                await _context.SaveChangesAsync(cancellationToken);
                
                user.ProfilePictureUrl = profileUrl;
            }
            
            if (request.UpdatePicturesDto.BackgroundPictureImage != null)
            {
                var backgroundUrl = await _blobStorageService.UploadImageAsync(request.UpdatePicturesDto.BackgroundPictureImage);
                var imageRecord = new ImageRecord { Url = backgroundUrl };
                _context.Images.Add(imageRecord);
                await _context.SaveChangesAsync(cancellationToken);
                
                user.BackgroundPictureUrl = backgroundUrl;
            }
            
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<EditedUserDTO>(user);
        }
    }