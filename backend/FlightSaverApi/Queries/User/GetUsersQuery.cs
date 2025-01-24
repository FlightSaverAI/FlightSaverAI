using System.Text.Json.Serialization;
using FlightSaverApi.DTOs.User;
using MediatR;

namespace FlightSaverApi.Queries.User;

public class GetUsersQuery : IRequest<IEnumerable<FriendDTO>>
{
    [JsonIgnore]
    public int UserId { get; set; }
}