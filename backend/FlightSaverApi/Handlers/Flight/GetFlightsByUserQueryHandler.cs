using AutoMapper;
using FlightSaverApi.Data;
using FlightSaverApi.Interfaces.Services;
using FlightSaverApi.Models.FlightModel;
using FlightSaverApi.Queries.Flight;
using FlightSaverApi.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers.Flight;

public class GetFlightsByUserQueryHandler : IRequestHandler<GetFlightsByUserQuery, IEnumerable<FlightDTO>>
{
    public readonly FlightSaverContext _context;
    public readonly IMapper _mapper;

    public GetFlightsByUserQueryHandler(FlightSaverContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<FlightDTO>> Handle(GetFlightsByUserQuery request, CancellationToken cancellationToken)
    {
        var flights = await _context.Flights
            .Where(f => f.UserId == request.UserId)
            .Include(f => f.AirportReviews)
            .Include(f => f.AirlineReview)
            .Include(f => f.AircraftReview)
            .ToListAsync(cancellationToken);

        return _mapper.Map<IEnumerable<FlightDTO>>(flights);
    }

}