using FlightSaverApi.DTOs.User;

namespace FlightSaverApi.Results;

public class PagedUserResult
{
    public IEnumerable<FriendDTO> Users { get; set; }
    public int TotalPages { get; set; }
}