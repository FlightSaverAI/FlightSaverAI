using FlightSaverApi.Attributes;
using FlightSaverApi.DTOs;
using MediatR;

namespace FlightSaverApi.Commands.Flight;

[SwaggerExclude]
public class UpdateFlightCommand : IRequest<FlightDTO>
{
    public int Id { get; set; }
    public FlightDTO FlightDto {get; set;}
}