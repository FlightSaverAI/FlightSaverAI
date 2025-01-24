namespace FlightSaverApi.DTOs.Post;

public class SocialPostDTO
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public DateTime PostedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Location { get; set; }
    public string Content { get; set; }
    public string ImageUrl { get; set; }
    public int LikesCount { get; set; }
    public int CommentsCount { get; set; }
}