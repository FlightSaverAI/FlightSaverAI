using MediatR;

namespace FlightSaverApi.Commands.Airport;

public class DeleteAirportCommand : IRequest<bool>
{
    public int Id { get; set; }    
}