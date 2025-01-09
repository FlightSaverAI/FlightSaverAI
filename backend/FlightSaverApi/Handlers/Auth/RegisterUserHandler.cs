using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using FlightSaverApi.Commands.Auth;
using FlightSaverApi.Data;
using FlightSaverApi.Enums;
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

    public RegisterUserHandler(FlightSaverContext context, IMapper mapper, IConfiguration configuration, ITokenService tokenService)
    {
        _context = context;
        _mapper = mapper;
        _tokenService = tokenService;
    }

    public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await _context.Users.AnyAsync(u => u.Email == request.UserRegisterDTO.Email))
        {
            throw new InvalidOperationException("Email already exists");
        }
        
        CreatePasswordHash(request.UserRegisterDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);
        
        var user = _mapper.Map<User>(request.UserRegisterDTO);
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        user.Role = UserRole.User;
        
        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        var token = _tokenService.CreateToken(user);

        return token;
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }
}