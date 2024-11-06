using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlightSaverApi.Data;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using NuGet.Protocol;
using FlightSaverApi.Models.UserModel;

namespace FlightSaverApi.Controllers
{
    [Route("/Auth")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly FlightSaverContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(FlightSaverContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // POST: /Auth/Register
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDTO request)
        {
            throw new NotImplementedException();
            //if (await _context.Users.AnyAsync(u => u.Email == request.Email))
            //{
            //    return BadRequest("Email already exists.");
            //}

            //CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            //var user = new User
            //{
            //    Username = request.Username,
            //    Role = Enums.UserRole.User,
            //    Email = request.Email,
            //    PasswordHash = passwordHash,
            //    PasswordSalt = passwordSalt
            //};

            //_context.Users.Add(user);
            //await _context.SaveChangesAsync();

            //var token = CreateToken(user);

            //return Ok(new { token });
        }

        // POST: /Auth/Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDTO request)
        {
            //var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            //if (user == null)
            //{
            //    return Unauthorized("Invalid email or password.");
            //}

            //if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            //{
            //    return Unauthorized("Invalid email or password.");
            //}

            //var token = CreateToken(user);
            //return Ok(new { token });
            throw new NotImplementedException();
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(storedHash);
            }
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKeyBase64 = jwtSettings["Secret"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];

            byte[] keyBytes;
            try
            {
                keyBytes = Convert.FromBase64String(secretKeyBase64);
            }
            catch (FormatException)
            {
                throw new ArgumentException("JWT Secret Key is not a valid Base64 string.");
            }

            if (keyBytes.Length < 65)
            {
                throw new ArgumentException("JWT Secret Key must be at least 65 bytes (520 bits) long.");
            }

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
}
