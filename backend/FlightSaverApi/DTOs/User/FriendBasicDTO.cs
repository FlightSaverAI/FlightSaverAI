namespace FlightSaverApi.DTOs.User;

public class FriendBasicDTO
{
    public int Id { get; set; }
    
    public string Name { get; set; }

    public string? ProfilePictureUrl { get; set; } = "";
}