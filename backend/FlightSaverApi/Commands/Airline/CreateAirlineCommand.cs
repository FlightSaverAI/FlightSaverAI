using FlightSaverApi.Attributes;
using FlightSaverApi.Models.AirlineModel;
using MediatR;

namespace FlightSaverApi.Commands.Airline;

[SwaggerExclude]
public class CreateAirlineCommand : IRequest<AirlineDTO>
{
    public AirlineDTO AirlineDto { get; set; }
}