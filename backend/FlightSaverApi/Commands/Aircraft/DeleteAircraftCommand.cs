using FlightSaverApi.Attributes;
using MediatR;

namespace FlightSaverApi.Commands.Aircraft;

[SwaggerExclude]
public class DeleteAircraftCommand : IRequest<Unit>
{
    public int Id { get; set; }
}