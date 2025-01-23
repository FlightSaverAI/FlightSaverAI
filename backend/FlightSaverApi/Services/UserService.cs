using System.Security.Cryptography;
using System.Text;
using FlightSaverApi.Interfaces.Services;

namespace FlightSaverApi.Services;

public class UserService : IUserService
{
    public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }
}