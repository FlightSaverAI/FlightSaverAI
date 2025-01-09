using FlightSaverApi.DTOs;
using MediatR;

namespace FlightSaverApi.Queries.Airport;

public class GetAirportsQuery : IRequest<IEnumerable<AirportDTO>>
{
}