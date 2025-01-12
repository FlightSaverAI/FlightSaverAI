using AutoMapper;
using FlightSaverApi.Data;
using FlightSaverApi.DTOs.Aircraft;
using FlightSaverApi.DTOs.Airport;
using FlightSaverApi.Queries.Airport;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers.Airport;

public class GetMinimalAirportsQueryHandler : IRequestHandler<GetMinimalAirportsQuery, IEnumerable<MinimalAirportDTO>>
{
    private readonly FlightSaverContext _context;
    private readonly IMapper _mapper;

    public GetMinimalAirportsQueryHandler(FlightSaverContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MinimalAirportDTO>> Handle(GetMinimalAirportsQuery request,
        CancellationToken cancellationToken)
    {
        var airports = await _context.Airports.ToListAsync(cancellationToken);
        
        return _mapper.Map<IEnumerable<MinimalAirportDTO>>(airports);
    }
}