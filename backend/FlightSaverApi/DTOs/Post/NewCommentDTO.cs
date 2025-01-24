using Newtonsoft.Json;

namespace FlightSaverApi.DTOs.Post;

public class NewCommentDTO
{
    public int PostId { get; set; }
    public string Content { get; set; }
    [JsonIgnore]
    public int? UserId { get; set; }
}