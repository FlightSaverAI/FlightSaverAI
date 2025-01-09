using AutoMapper;
using FlightSaverApi.Data;
using FlightSaverApi.DTOs;
using FlightSaverApi.Queries.Airport;
using MediatR;

namespace FlightSaverApi.Handlers.Airport;

public class GetAirportQueryHandler : IRequestHandler<GetAirportQuery, AirportDTO>
{
    private readonly FlightSaverContext _context;
    private readonly IMapper _mapper;

    public GetAirportQueryHandler(FlightSaverContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AirportDTO> Handle(GetAirportQuery request, CancellationToken cancellationToken)
    {
        var airport = await _context.Airports.FindAsync(request.Id);

        if (airport == null)
        {
            throw new KeyNotFoundException($"Airport with Id {request.Id} does not exist.");
        }
        
        var airportDto = _mapper.Map<AirportDTO>(airport);
        
        return airportDto;
    }
}