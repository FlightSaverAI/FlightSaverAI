using AutoMapper;
using FlightSaverApi.Data;
using FlightSaverApi.DTOs.Flight;
using FlightSaverApi.Interfaces.Services;
using FlightSaverApi.Queries.Flight;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers.Flight;

public class GetMinimalFlightsByUserQueryHandler : IRequestHandler<GetMinimalFlightsByUserQuery, IEnumerable<MinimalFlightDTO>>
{
    public readonly FlightSaverContext _context;
    public readonly IMapper _mapper;
    
    public GetMinimalFlightsByUserQueryHandler(FlightSaverContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MinimalFlightDTO>> Handle(GetMinimalFlightsByUserQuery request,
        CancellationToken cancellationToken)
    {
        var flights = await _context.Flights
            .Where(f => f.UserId == request.UserId)
            .Include(f => f.DepartureAirport)
            .Include(f => f.ArrivalAirport)
            .ToListAsync(cancellationToken);
        
        return _mapper.Map<IEnumerable<MinimalFlightDTO>>(flights);
    }
}