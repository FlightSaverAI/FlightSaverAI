namespace FlightSaverApi.DTOs.Post;

public class EditSocialPostDTO
{
    public int Id { get; set; }
    public string? Location { get; set; }
    public string? Content { get; set; }
    public IFormFile? Image { get; set; }
    public string? ImageUrl { get; set; }
}