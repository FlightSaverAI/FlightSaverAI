using FlightSaverApi.Data;
using FlightSaverApi.Interfaces.Services;
using FlightSaverApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Services;

public class FlightsService : IFlightsService
{
    private readonly FlightSaverContext _context;

    public FlightsService(FlightSaverContext context)
    {
        _context = context;
    }
    
    public async Task<List<Flight>> GetFlightsByUserIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        var flights = await _context.Flights
            .Where(f => f.UserId == userId)
            .Include(f => f.DepartureAirport)
            .Include(f => f.ArrivalAirport)
            .Include(f => f.Airline)
            .Include(f => f.Aircraft)
            .ToListAsync(cancellationToken);
        
        return flights;
    }
}