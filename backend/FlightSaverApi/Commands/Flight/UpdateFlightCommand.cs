using FlightSaverApi.Models.FlightModel;
using MediatR;

namespace FlightSaverApi.Commands.Flight;

public class UpdateFlightCommand : IRequest<FlightDTO>
{
    public int Id { get; set; }
    public FlightDTO FlightDto {get; set;}
}