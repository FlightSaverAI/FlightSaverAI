using FlightSaverApi.Models.FlightModel;
using FlightSaverApi.Queries.Airport;
using MediatR;

namespace FlightSaverApi.Queries.Flight;

public class GetFlightQuery : IRequest<FlightDTO>
{
    public int Id { get; set; }

    public GetFlightQuery(int id)
    {
        Id = id;
    }
}