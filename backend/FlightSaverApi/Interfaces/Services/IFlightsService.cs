using FlightSaverApi.Models;

namespace FlightSaverApi.Interfaces.Services;

public interface IFlightsService
{
    Task<List<Flight>> GetFlightsByUserIdAsync(int userId, CancellationToken cancellationToken);
}