using AutoMapper;
using FlightSaverApi.Data;
using FlightSaverApi.DTOs;
using FlightSaverApi.Queries.Airport;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers.Airport;

public class GetAirportsQueryHandler : IRequestHandler<GetAirportsQuery, IEnumerable<AirportDTO>>
{
    public readonly FlightSaverContext _context;
    public readonly IMapper _mapper;

    public GetAirportsQueryHandler(FlightSaverContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;        
    }

    public async Task<IEnumerable<AirportDTO>> Handle(GetAirportsQuery request, CancellationToken cancellationToken)
    {
        var airports = await _context.Airports.ToListAsync(cancellationToken);
        
        return _mapper.Map<IEnumerable<AirportDTO>>(airports);
    }
}