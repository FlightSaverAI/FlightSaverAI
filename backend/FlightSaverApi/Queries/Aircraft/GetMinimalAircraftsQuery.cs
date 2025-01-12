using FlightSaverApi.DTOs.Aircraft;
using MediatR;

namespace FlightSaverApi.Queries.Aircraft;

public class GetMinimalAircraftsQuery : IRequest<IEnumerable<MinimalAircraftDTO>>
{
    
}