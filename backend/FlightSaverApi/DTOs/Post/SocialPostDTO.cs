using System.Text.Json.Serialization;
using FlightSaverApi.DTOs.User;

namespace FlightSaverApi.DTOs.Post;

public class SocialPostDTO
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public EditedUserDTO User { get; set; }
    public DateTime PostedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string Location { get; set; }
    public string Content { get; set; }
    public string ImageUrl { get; set; }
    public int LikesCount { get; set; }
    public int CommentsCount { get; set; }
    public bool IsLikedByCurrentUser { get; set; }
}