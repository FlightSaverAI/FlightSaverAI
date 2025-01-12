using AutoMapper;
using FlightSaverApi.Data;
using FlightSaverApi.DTOs.Aircraft;
using FlightSaverApi.Queries.Aircraft;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers.Aircraft;

public class GetMinimalAircraftsQueryHandler : IRequestHandler<GetMinimalAircraftsQuery, IEnumerable<MinimalAircraftDTO>>
{
    private readonly FlightSaverContext _context;
    private readonly IMapper _mapper;
    
    public GetMinimalAircraftsQueryHandler(FlightSaverContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MinimalAircraftDTO>> Handle(GetMinimalAircraftsQuery request,
        CancellationToken cancellationToken)
    {
        var aircrafts = await _context.Aircrafts.ToListAsync(cancellationToken);
        
        return _mapper.Map<IEnumerable<MinimalAircraftDTO>>(aircrafts);
    }
}