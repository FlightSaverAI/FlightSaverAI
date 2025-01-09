using FlightSaverApi.Attributes;
using FlightSaverApi.DTOs;
using MediatR;

namespace FlightSaverApi.Commands.Airport;

[SwaggerExclude]
public class CreateAirportCommand : IRequest<AirportDTO>
{
    public AirportDTO AirportDto { get; set; }  
}