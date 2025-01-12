using FlightSaverApi.Attributes;
using FlightSaverApi.DTOs;
using FlightSaverApi.DTOs.Airline;
using MediatR;

namespace FlightSaverApi.Commands.Airline;

[SwaggerExclude]
public class CreateAirlineCommand : IRequest<AirlineDTO>
{
    public AirlineDTO AirlineDto { get; set; }
}