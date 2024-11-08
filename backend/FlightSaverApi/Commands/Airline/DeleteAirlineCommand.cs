using MediatR;

namespace FlightSaverApi.Commands.Airline;

public class DeleteAirlineCommand : IRequest<bool>
{
    public int Id { get; set; }
}