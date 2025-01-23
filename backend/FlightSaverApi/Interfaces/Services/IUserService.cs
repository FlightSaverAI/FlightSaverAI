namespace FlightSaverApi.Interfaces.Services;

public interface IUserService
{
    void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
}