using FlightSaverApi.DTOs;
using MediatR;

namespace FlightSaverApi.Queries.Flight;

public class GetFlightsQuery : IRequest<IEnumerable<FlightDTO>>
{
}