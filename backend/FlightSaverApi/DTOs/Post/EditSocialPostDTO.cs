namespace FlightSaverApi.DTOs.Post;

public class EditSocialPostDTO
{
    public int id { get; set; }
    public string? location { get; set; }
    public string? content { get; set; }
    public IFormFile? image { get; set; }
}