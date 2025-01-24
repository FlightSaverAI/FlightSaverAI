namespace FlightSaverApi.Interfaces.Services;

public interface IBlobStorageService
{
    Task<string> UploadImageAsync(IFormFile image);
}