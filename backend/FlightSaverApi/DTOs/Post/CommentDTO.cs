namespace FlightSaverApi.DTOs.Post;

public class CommentDTO
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public int SocialPostId { get; set; }
    public DateTime PostedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Content { get; set; }
    public int LikesCount { get; set; }
}