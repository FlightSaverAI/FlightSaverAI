using FlightSaverApi.Attributes;
using FlightSaverApi.DTOs;
using FlightSaverApi.DTOs.Aircraft;
using MediatR;

namespace FlightSaverApi.Commands.Aircraft;

[SwaggerExclude]
public class UpdateAircraftCommand : IRequest<AircraftDTO>
{
    public int Id { get; set; }
    public AircraftDTO AircraftDto { get; set; }
}