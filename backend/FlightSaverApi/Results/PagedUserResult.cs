using FlightSaverApi.DTOs.User;

namespace FlightSaverApi.Results;

public class PagedUserResult
{
    public IEnumerable<FriendDTO> Users { get; set; } = new List<FriendDTO>();
    public int TotalPages { get; set; }
}