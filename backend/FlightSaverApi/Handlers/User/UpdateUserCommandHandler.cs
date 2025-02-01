using AutoMapper;
using FlightSaverApi.Commands.User;
using FlightSaverApi.Data;
using FlightSaverApi.DTOs.Aircraft;
using FlightSaverApi.DTOs.User;
using FlightSaverApi.Interfaces.Services;
using FlightSaverApi.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers.User;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, EditedUserDTO>
{
    private readonly FlightSaverContext _context;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly IBlobStorageService _blobStorageService;

    public UpdateUserCommandHandler(FlightSaverContext context, IMapper mapper, IUserService userService, IBlobStorageService blobStorageService)
    {
        _context = context;
        _mapper = mapper;
        _userService = userService;
        _blobStorageService = blobStorageService;
    }
    
    public async Task<EditedUserDTO> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(a => a.Id == request.UserId, cancellationToken);

        if (user == null)
        {
            throw new KeyNotFoundException($"User with Id {request.UserId} does not exist.");
        }
        
        if (!string.IsNullOrEmpty(request.EditUserDto.Password))
        {
            _userService.CreatePasswordHash(request.EditUserDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
        }

        if (request.EditUserDto.ProfilePictureImage != null)
        {
            var profileUrl = await _blobStorageService.UploadImageAsync(request.EditUserDto.ProfilePictureImage);

            var imageRecord = new ImageRecord
            {
                Url = profileUrl,
            };
            
            _context.Images.Add(imageRecord);
             await _context.SaveChangesAsync(cancellationToken);
             
             user.ProfilePictureUrl = profileUrl;
        }
        
        if (request.EditUserDto.BackgroundPictureImage != null)
        {
            var backgroundUrl = await _blobStorageService.UploadImageAsync(request.EditUserDto.BackgroundPictureImage);

            var imageRecord = new ImageRecord()
            {
                Url = backgroundUrl
            };
            
            _context.Images.Add(imageRecord);
            await _context.SaveChangesAsync(cancellationToken);
            
            user.BackgroundPictureUrl = backgroundUrl;
        }

        _mapper.Map(request.EditUserDto, user);

        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<EditedUserDTO>(user);
    }
}