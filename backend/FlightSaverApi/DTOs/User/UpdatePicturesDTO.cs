namespace FlightSaverApi.DTOs.User;

public class UpdatePicturesDTO
{
    public IFormFile? ProfilePictureImage { get; set; }
    public IFormFile? BackgroundPictureImage { get; set; }
}