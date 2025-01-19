using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using FlightSaverApi.Commands.Auth;
using FlightSaverApi.Data;
using FlightSaverApi.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers.Auth;

public class LoginUserHandler : IRequestHandler<LoginUserCommand, string>
{
    private readonly FlightSaverContext _context;
    private readonly ITokenService _tokenService;

    public LoginUserHandler(FlightSaverContext context, IMapper mapper, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.UserLoginDTO.Email, cancellationToken);
        if(user == null || !VerifyPasswordHash(request.UserLoginDTO.Password, user.PasswordHash, user.PasswordSalt))
        {
            throw new UnauthorizedAccessException("Invalid email or password");
        }
        
        var token = _tokenService.CreateToken(user);
        return token;
    }
    
    private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
    {
        using (var hmac = new HMACSHA512(storedSalt))
        {
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(storedHash);
        }
    }
}