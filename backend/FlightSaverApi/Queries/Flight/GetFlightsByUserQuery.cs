using FlightSaverApi.DTOs;
using FlightSaverApi.DTOs.Flight;
using MediatR;

namespace FlightSaverApi.Queries.Flight;

public class GetFlightsByUserQuery : IRequest<IEnumerable<FlightDTO>>
{
    public int UserId { get; }
    
    public string? Includes { get; }

    public GetFlightsByUserQuery(int userId, string? includes)
    {
        UserId = userId;
        Includes = includes;
    }
}