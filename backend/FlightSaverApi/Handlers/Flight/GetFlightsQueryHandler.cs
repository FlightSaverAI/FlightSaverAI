using AutoMapper;
using FlightSaverApi.Data;
using FlightSaverApi.Models.FlightModel;
using FlightSaverApi.Queries.Flight;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers.Flight;

public class GetFlightsQueryHandler : IRequestHandler<GetFlightsQuery, IEnumerable<FlightDTO>>
{
    public readonly FlightSaverContext _context;
    public readonly IMapper _mapper;

    public GetFlightsQueryHandler(FlightSaverContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;        
    }

    public async Task<IEnumerable<FlightDTO>> Handle(GetFlightsQuery request, CancellationToken cancellationToken)
    {
        var flights = await _context.Flights
            .Include(f => f.AirportReviews)
            .Include(f => f.AirlineReview)
            .Include(f => f.AircraftReview)
            .ToListAsync(cancellationToken);
        
        return _mapper.Map<IEnumerable<FlightDTO>>(flights);
    }
}