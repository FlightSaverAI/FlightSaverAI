using FlightSaverApi.Models.AirlineModel;
using MediatR;

namespace FlightSaverApi.Commands.Airline;

public class CreateAirlineCommand : IRequest<AirlineDTO>
{
    public AirlineDTO AirlineDto { get; set; }
}