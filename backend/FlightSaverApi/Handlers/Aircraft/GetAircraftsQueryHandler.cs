using AutoMapper;
using FlightSaverApi.Data;
using FlightSaverApi.DTOs;
using FlightSaverApi.Queries.Aircraft;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers;

public class GetAircraftsQueryHandler : IRequestHandler<GetAircraftsQuery, IEnumerable<AircraftDTO>>
{
    private readonly FlightSaverContext _context;
    private readonly IMapper _mapper;

    public GetAircraftsQueryHandler(FlightSaverContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AircraftDTO>> Handle(GetAircraftsQuery request, CancellationToken cancellationToken)
    {
        var aircrafts = await _context.Aircrafts.ToListAsync(cancellationToken);
        
        return _mapper.Map<IEnumerable<AircraftDTO>>(aircrafts);
    }
}