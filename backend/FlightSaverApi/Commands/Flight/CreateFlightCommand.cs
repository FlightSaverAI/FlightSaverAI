using FlightSaverApi.Attributes;
using FlightSaverApi.DTOs.Aircraft;
using FlightSaverApi.DTOs.Flight;
using MediatR;

namespace FlightSaverApi.Commands.Flight;

[SwaggerExclude]
public class CreateFlightCommand : IRequest<NewFlightDTO>
{
    public NewFlightDTO NewFlightDTO { get; set; }
}