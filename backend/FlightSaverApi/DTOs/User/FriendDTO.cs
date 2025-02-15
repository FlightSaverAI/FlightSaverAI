using FlightSaverApi.Models;

namespace FlightSaverApi.DTOs.User;

public class FriendDTO
{
    public int Id { get; set; }
    
    public string Name { get; set; }

    public string? ProfilePictureUrl { get; set; } = "";

    public string? BackgroundPictureUrl { get; set; } = "";
    
    public bool IsLoggedUserFriend { get; set; }
    
    public BasicFlightStatistics Statistics { get; set; }
}