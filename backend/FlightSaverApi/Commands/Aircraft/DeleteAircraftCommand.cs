using MediatR;

namespace FlightSaverApi.Commands.Aircraft;

public class DeleteAircraftCommand : IRequest<Unit>
{
    public int Id { get; set; }
}