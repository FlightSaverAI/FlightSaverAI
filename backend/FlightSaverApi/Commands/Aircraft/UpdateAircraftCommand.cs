using MediatR;

namespace FlightSaverApi.Commands.Aircraft;

public class UpdateAircraftCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string IataCode { get; set; }
    public string IcaoCode { get; set; }
    public string RegNumber { get; set; }
    public string? AircraftUrl { get; set; }
    public int AirlineId { get; set; }
}