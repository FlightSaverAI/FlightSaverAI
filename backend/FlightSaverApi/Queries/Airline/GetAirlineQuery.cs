using FlightSaverApi.DTOs;
using MediatR;

namespace FlightSaverApi.Queries.Airline;

public class GetAirlineQuery : IRequest<AirlineDTO>
{
    public int Id { get; set; }

    public GetAirlineQuery(int id)
    {
        Id = id;
    }
}