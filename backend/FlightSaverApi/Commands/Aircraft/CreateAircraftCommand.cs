using FlightSaverApi.Models.AircraftModel;
using MediatR;

namespace FlightSaverApi.Commands.Aircraft;

public class CreateAircraftCommand : IRequest<AircraftDTO>
{
    public string Name { get; set; }
    public string IataCode { get; set; }
    public string IcaoCode { get; set; }
    public string RegNumber { get; set; }
    public string? AircraftUrl { get; set; }
    public int AirlineId { get; set; }
}