using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FlightSaverApi.Models.UserModel;
using Microsoft.IdentityModel.Tokens;

namespace FlightSaverApi.Services;

public interface ITokenService
{
    string CreateToken(User user);
}

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim("name", user.Username),
            new Claim("email", user.Email),
            new Claim("role", user.Role.ToString()),
            new Claim("id", user.Id.ToString()),
        };
        
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var secretKeyBase64 = jwtSettings["Secret"];
        var issuer = jwtSettings["Issuer"];
        var audience = jwtSettings["Audience"];
        var keyBytes = Convert.FromBase64String(secretKeyBase64);
        var key = new SymmetricSecurityKey(keyBytes);
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }
}