using Newtonsoft.Json;

namespace FlightSaverApi.DTOs.Post;

public class NewCommentDTO
{
    public int postId { get; set; }
    public string content { get; set; }
}