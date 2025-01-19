using FlightSaverApi.DTOs;
using FlightSaverApi.DTOs.Aircraft;
using MediatR;

namespace FlightSaverApi.Queries.Aircraft;

public class GetAircraftsQuery : IRequest<IEnumerable<AircraftDTO>>
{
    
}