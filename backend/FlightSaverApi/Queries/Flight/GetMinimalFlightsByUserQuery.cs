using FlightSaverApi.DTOs.Flight;
using MediatR;

namespace FlightSaverApi.Queries.Flight;

public class GetMinimalFlightsByUserQuery : IRequest<IEnumerable<MinimalFlightDTO>>
{
    public int UserId { get; } 

    public GetMinimalFlightsByUserQuery(int userId)
    {
        UserId = userId;
    }
}