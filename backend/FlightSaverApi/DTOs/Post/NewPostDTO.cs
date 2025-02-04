using Newtonsoft.Json;

namespace FlightSaverApi.DTOs.Post;

public class NewPostDTO
{
    public string location { get; set; }
    public string content { get; set; }
    public IFormFile? image { get; set; }
}