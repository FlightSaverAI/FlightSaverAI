using FlightSaverApi.Models.AirlineModel;
using MediatR;

namespace FlightSaverApi.Commands.Airline;

public class UpdateAirlineCommand : IRequest<AirlineDTO>
{
    public int Id { get; set; }
    public AirlineDTO AirlineDto { get; set; }
}