using AutoMapper;
using FlightSaverApi.Commands.User;
using FlightSaverApi.Data;
using FlightSaverApi.DTOs.User;
using FlightSaverApi.Interfaces.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers.User;

public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, EditedUserDTO>
{
    private readonly FlightSaverContext _context;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UpdatePasswordCommandHandler(FlightSaverContext context, IUserService userService, IMapper mapper)
    {
        _context = context;
        _userService = userService;
        _mapper = mapper;
    }
        
    public async Task<EditedUserDTO> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(a => a.Id == request.UserId, cancellationToken);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with Id {request.UserId} does not exist.");
        }

        // Create new password hash and salt
        _userService.CreatePasswordHash(request.UpdatePasswordDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<EditedUserDTO>(user);
    }
}