using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using FlightSaverApi.Commands.Auth;
using FlightSaverApi.Data;
using FlightSaverApi.Enums;
using FlightSaverApi.Interfaces.Services;
using FlightSaverApi.Models;
using FlightSaverApi.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers.Auth;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, string>
{
    private readonly FlightSaverContext _context;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;
    private readonly IUserService _userService;

    public RegisterUserHandler(FlightSaverContext context, IMapper mapper, IConfiguration configuration, ITokenService tokenService, IUserService userService)
    {
        _context = context;
        _mapper = mapper;
        _tokenService = tokenService;
        _userService = userService;
    }

    public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await _context.Users.AnyAsync(u => u.Email == request.UserRegisterDTO.Email))
        {
            throw new InvalidOperationException("Email already exists");
        }
        
        _userService.CreatePasswordHash(request.UserRegisterDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);
        
        var user = _mapper.Map<Models.User>(request.UserRegisterDTO);
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        user.Role = UserRole.User;
        user.ProfilePictureUrl = "";
        user.BackgroundPictureUrl = "";
        
        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        var token = _tokenService.CreateToken(user);

        return token;
    }
}