namespace FlightSaverApi.Models;

public class SocialPost
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public virtual User User { get; set; }
    public DateTime PostedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Location { get; set; }
    public string Content { get; set; }
    public string ImageUrl { get; set; }
    public int LikesCount { get; set; }
    public int CommentsCount { get; set; }
    public virtual List<Comment>? Comments { get; set; }
}