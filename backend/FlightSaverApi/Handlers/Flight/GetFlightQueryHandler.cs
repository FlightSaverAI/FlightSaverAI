using AutoMapper;
using FlightSaverApi.Data;
using FlightSaverApi.DTOs;
using FlightSaverApi.Queries.Flight;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers.Flight;

public class GetFlightQueryHandler : IRequestHandler<GetFlightQuery, FlightDTO>
{
    private readonly FlightSaverContext _context;
    private readonly IMapper _mapper;

    public GetFlightQueryHandler(FlightSaverContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<FlightDTO> Handle(GetFlightQuery request, CancellationToken cancellationToken)
    {
        var flight = await _context.Flights
            .Include(f => f.AirportReviews)     // Eagerly load AirportReviews
            .Include(f => f.AirlineReview)      // Eagerly load AirlineReview
            .Include(f => f.AircraftReview)     // Eagerly load AircraftReview
            .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);

        if (flight == null)
        {
            throw new KeyNotFoundException($"Flight with Id {request.Id} does not exist.");
        }
        
        var flightDto = _mapper.Map<FlightDTO>(flight);
        
        return flightDto;
    }
}