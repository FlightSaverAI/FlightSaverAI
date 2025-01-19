using AutoMapper;
using FlightSaverApi.Data;
using FlightSaverApi.DTOs.Airline;
using FlightSaverApi.Queries.Airline;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers.Airline;

public class GetMinimalAirlinesQueryHandler : IRequestHandler<GetMinimalAirlinesQuery, IEnumerable<MinimalAirlineDTO>>
{
    private readonly FlightSaverContext _context;
    private readonly IMapper _mapper;

    public GetMinimalAirlinesQueryHandler(FlightSaverContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MinimalAirlineDTO>> Handle(GetMinimalAirlinesQuery request,
        CancellationToken cancellationToken)
    {
        var airlines = await _context.Airlines.ToListAsync(cancellationToken);
        
        return _mapper.Map<IEnumerable<MinimalAirlineDTO>>(airlines);
    }
}