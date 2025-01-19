using FlightSaverApi.DTOs;
using FlightSaverApi.DTOs.Airline;
using MediatR;

namespace FlightSaverApi.Queries.Airline;

public class GetAirlinesQuery : IRequest<IEnumerable<AirlineDTO>>
{
}