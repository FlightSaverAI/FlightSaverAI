using FlightSaverApi.Attributes;
using MediatR;

namespace FlightSaverApi.Commands.Flight;

[SwaggerExclude]
public class DeleteFlightCommand : IRequest<bool>
{
    public int Id { get; set; }
}