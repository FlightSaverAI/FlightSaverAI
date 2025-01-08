using FlightSaverApi.Models.FlightModel;
using MediatR;

namespace FlightSaverApi.Queries.Flight;

public class GetFlightsByUserQuery : IRequest<IEnumerable<FlightDTO>>
{
    public int UserId { get; }

    public GetFlightsByUserQuery(int userId)
    {
        UserId = userId;
    }
}