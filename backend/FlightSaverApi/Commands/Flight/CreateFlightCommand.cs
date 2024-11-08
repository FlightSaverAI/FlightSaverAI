using FlightSaverApi.Models.FlightModel;
using MediatR;

namespace FlightSaverApi.Commands.Flight;

public class CreateFlightCommand : IRequest<FlightDTO>
{
    public FlightDTO FlightDto { get; set; }
}