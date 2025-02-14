namespace FlightSaverApi.DTOs.User;

public class UpdatePicturesDTO
{
    public IFormFile? ProfilePictureImage { get; set; }
    public bool RemoveProfilePicture { get; set; }
    public IFormFile? BackgroundPictureImage { get; set; }
    public bool RemoveBackgroundPicture { get; set; }
}