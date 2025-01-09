using FlightSaverApi.DTOs;
using MediatR;

namespace FlightSaverApi.Queries.Airline;

public class GetAirlinesQuery : IRequest<IEnumerable<AirlineDTO>>
{
}