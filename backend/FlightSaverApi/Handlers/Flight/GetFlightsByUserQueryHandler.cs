using AutoMapper;
using FlightSaverApi.Data;
using FlightSaverApi.DTOs;
using FlightSaverApi.Interfaces.Services;
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

        var query = _context.Flights
            .Where(f => f.UserId == request.UserId)
            .AsQueryable();

        if (!string.IsNullOrEmpty(request.Includes))
        {
            var includes = request.Includes.Split(',');

            foreach (var includeItem in includes)
            {
                switch (includeItem.Trim().ToLower())
                {
                    case "airports":
                        query = query.Include(f => f.DepartureAirport);
                        query = query.Include(f => f.ArrivalAirport);
                        break;
                    case "airlines":
                        query = query.Include(f => f.Airline);
                        break;
                    case "aircrafts":
                        query = query.Include(f => f.Aircraft);
                        break;
                    case "reviews":
                        query = query.Include(f => f.AirportReviews);
                        query = query.Include(f => f.AircraftReview);
                        query = query.Include(f => f.AircraftReview);
                        break;
                }
                
            }
        }
        
        var flights = await query.ToListAsync(cancellationToken);

        return _mapper.Map<IEnumerable<FlightDTO>>(flights);
    }

}