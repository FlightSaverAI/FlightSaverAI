namespace FlightSaverApi.DTOs.Post;

public class EditSocialPostImageDTO
{
    public int id { get; set; }
    public IFormFile? image { get; set; }
}