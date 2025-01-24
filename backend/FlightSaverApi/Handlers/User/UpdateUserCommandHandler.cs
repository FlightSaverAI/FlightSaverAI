using AutoMapper;
using FlightSaverApi.Commands.User;
using FlightSaverApi.Data;
using FlightSaverApi.DTOs.Aircraft;
using FlightSaverApi.DTOs.User;
using FlightSaverApi.Interfaces.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers.User;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, EditUserDTO>
{
    private readonly FlightSaverContext _context;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public UpdateUserCommandHandler(FlightSaverContext context, IMapper mapper, IUserService userService)
    {
        _context = context;
        _mapper = mapper;
        _userService = userService;
    }
    
    public async Task<EditUserDTO> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
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

        _mapper.Map(request.EditUserDto, user);

        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<EditUserDTO>(user);
    }
}