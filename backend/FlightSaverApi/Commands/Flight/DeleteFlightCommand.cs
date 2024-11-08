using MediatR;

namespace FlightSaverApi.Commands.Flight;

public class DeleteFlightCommand : IRequest<bool>
{
    public int Id { get; set; }
}