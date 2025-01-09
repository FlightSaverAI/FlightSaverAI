using FlightSaverApi.DTOs;
using FlightSaverApi.DTOs.Flight;
using MediatR;

namespace FlightSaverApi.Queries.Flight;

public class GetFlightsQuery : IRequest<IEnumerable<FlightDTO>>
{
}