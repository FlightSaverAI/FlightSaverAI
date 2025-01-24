using Newtonsoft.Json;

namespace FlightSaverApi.DTOs.Post;

public class NewPostDTO
{
    public string Location { get; set; }
    public string Content { get; set; }
    public string ImageUrl { get; set; }
    [JsonIgnore]
    public int? UserId { get; set; }
}