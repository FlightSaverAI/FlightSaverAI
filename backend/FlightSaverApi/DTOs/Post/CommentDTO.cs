using FlightSaverApi.DTOs.User;

namespace FlightSaverApi.DTOs.Post;

public class CommentDTO
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public EditedUserDTO User { get; set; }
    public DateTime PostedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string Content { get; set; }
    public int LikesCount { get; set; }
    public bool IsLikedByCurrentUser { get; set; }
}