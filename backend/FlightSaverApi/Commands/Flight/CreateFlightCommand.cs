using FlightSaverApi.Attributes;
using FlightSaverApi.Models.FlightModel;
using MediatR;

namespace FlightSaverApi.Commands.Flight;

[SwaggerExclude]
public class CreateFlightCommand : IRequest<FlightDTO>
{
    public FlightDTO FlightDto { get; set; }
}