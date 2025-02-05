using System.Text.Json.Serialization;
using FlightSaverApi.DTOs.User;
using FlightSaverApi.Results;
using MediatR;

namespace FlightSaverApi.Queries.User;

public class GetUsersQuery : IRequest<PagedUserResult>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
    public string? Name { get; set; }
}