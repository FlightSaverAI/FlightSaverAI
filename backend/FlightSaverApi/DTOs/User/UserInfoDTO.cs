namespace FlightSaverApi.DTOs.User;

public class UserInfoDTO
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public int FriendsCount { get; set; } = 0;
    public string? Password { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public string? BackgroundPictureUrl { get; set; }
}