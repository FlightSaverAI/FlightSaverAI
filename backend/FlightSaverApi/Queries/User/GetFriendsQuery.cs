using System.Text.Json.Serialization;
using FlightSaverApi.DTOs.User;
using FlightSaverApi.Enums;
using FlightSaverApi.Results;
using MediatR;

namespace FlightSaverApi.Queries.User;

public class GetFriendsQuery : IRequest<PagedUserResult>
{
    [JsonIgnore]
    public int UserId { get; set; }
    [JsonIgnore]
    public UserRole UserRole { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
    public string? Name { get; set; }
}