using FlightSaverApi.Attributes;
using FlightSaverApi.DTOs;
using FlightSaverApi.DTOs.Airline;
using MediatR;

namespace FlightSaverApi.Commands.Airline;

[SwaggerExclude]
public class UpdateAirlineCommand : IRequest<AirlineDTO>
{
    public int Id { get; set; }
    public AirlineDTO AirlineDto { get; set; }
}