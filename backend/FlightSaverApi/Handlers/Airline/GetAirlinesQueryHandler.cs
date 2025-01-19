using AutoMapper;
using FlightSaverApi.Data;
using FlightSaverApi.DTOs;
using FlightSaverApi.DTOs.Airline;
using FlightSaverApi.Queries.Airline;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers.Airline;

public class GetAirlinesQueryHandler : IRequestHandler<GetAirlinesQuery, IEnumerable<AirlineDTO>>
{
    public readonly FlightSaverContext _context;
    public readonly IMapper _mapper;

    public GetAirlinesQueryHandler(FlightSaverContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;        
    }

    public async Task<IEnumerable<AirlineDTO>> Handle(GetAirlinesQuery request, CancellationToken cancellationToken)
    {
        var airlines = await _context.Airlines.ToListAsync(cancellationToken);
        
        return _mapper.Map<IEnumerable<AirlineDTO>>(airlines);
    }
}