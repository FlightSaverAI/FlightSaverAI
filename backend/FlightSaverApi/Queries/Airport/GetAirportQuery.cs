using FlightSaverApi.Models.AirportModel;
using MediatR;

namespace FlightSaverApi.Queries.Airport;

public class GetAirportQuery : IRequest<AirportDTO>
{
    public int Id { get; set; }

    public GetAirportQuery(int id)
    {
        Id = id;
    }
}