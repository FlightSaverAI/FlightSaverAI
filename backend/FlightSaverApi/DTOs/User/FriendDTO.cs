using FlightSaverApi.Models;

namespace FlightSaverApi.DTOs.User;

public class FriendDTO
{
    public string Name { get; set; }
    
    public BasicFlightStatistics Statistics { get; set; }
}