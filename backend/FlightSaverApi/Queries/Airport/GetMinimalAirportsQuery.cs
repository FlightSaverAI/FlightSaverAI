using FlightSaverApi.DTOs.Aircraft;
using FlightSaverApi.DTOs.Airport;
using MediatR;

namespace FlightSaverApi.Queries.Airport;

public class GetMinimalAirportsQuery : IRequest<IEnumerable<MinimalAirportDTO>>
{
    
}