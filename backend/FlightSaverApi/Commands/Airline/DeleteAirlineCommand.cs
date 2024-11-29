using FlightSaverApi.Attributes;
using MediatR;

namespace FlightSaverApi.Commands.Airline;

[SwaggerExclude]
public class DeleteAirlineCommand : IRequest<bool>
{
    public int Id { get; set; }
}