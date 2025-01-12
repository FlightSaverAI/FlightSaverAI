using FlightSaverApi.DTOs.Airline;
using MediatR;

namespace FlightSaverApi.Queries.Airline;

public class GetMinimalAirlinesQuery : IRequest<IEnumerable<MinimalAirlineDTO>>
{
    
}