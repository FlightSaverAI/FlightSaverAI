using FlightSaverApi.Attributes;
using MediatR;

namespace FlightSaverApi.Commands.Airport;

[SwaggerExclude]
public class DeleteAirportCommand : IRequest<bool>
{
    public int Id { get; set; }    
}