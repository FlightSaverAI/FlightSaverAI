using FlightSaverApi.Models.AircraftModel;
using MediatR;

namespace FlightSaverApi.Queries.Aircraft;

public class GetAircraftQuery : IRequest<AircraftDTO>
{
    public int Id { get; set; }

    public GetAircraftQuery(int id)
    {
        Id = id;
    }
}