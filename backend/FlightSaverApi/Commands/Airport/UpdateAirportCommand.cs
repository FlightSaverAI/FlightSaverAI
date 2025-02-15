using FlightSaverApi.Attributes;
using FlightSaverApi.DTOs;
using FlightSaverApi.DTOs.Airport;
using MediatR;

namespace FlightSaverApi.Commands.Airport;

[SwaggerExclude]
public class UpdateAirportCommand : IRequest<AirportDTO>
{
    public int Id { get; set; }
    public AirportDTO AirportDto { get; set; }
}