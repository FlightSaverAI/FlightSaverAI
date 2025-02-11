using System.Text.Json.Serialization;
using FlightSaverApi.DTOs.User;
using FlightSaverApi.Enums;
using MediatR;

namespace FlightSaverApi.Queries.User;

public class GetFriendsBasicQuery : IRequest<IEnumerable<FriendBasicDTO>>
{
    [JsonIgnore]
    public int UserId { get; set; }
    [JsonIgnore]
    public UserRole UserRole { get; set; }
}