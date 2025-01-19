using FlightSaverApi.DTOs;
using FlightSaverApi.DTOs.Airport;
using MediatR;

namespace FlightSaverApi.Queries.Airport;

public class GetAirportsQuery : IRequest<IEnumerable<AirportDTO>>
{
}