namespace FlightSaverApi.Models;

public class Comment
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public virtual User User { get; set; }
    public DateTime PostedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string Content { get; set; }
    public int LikesCount { get; set; }
    public int SocialPostId {get; set;}
    public virtual SocialPost SocialPost { get; set; }
    public virtual List<CommentLike> Likes { get; set; }
}

public class CommentLike
{
    public int Id { get; set; }
    public int CommentId { get; set; }
    public virtual Comment Comment { get; set; }
    public int UserId { get; set; }
    public virtual User User { get; set; }
}