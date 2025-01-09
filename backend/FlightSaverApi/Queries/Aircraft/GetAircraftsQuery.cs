using FlightSaverApi.DTOs;
using MediatR;

namespace FlightSaverApi.Queries.Aircraft;

public class GetAircraftsQuery : IRequest<IEnumerable<AircraftDTO>>
{
    
}