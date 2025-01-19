using AutoMapper;
using FlightSaverApi.Data;
using FlightSaverApi.DTOs;
using FlightSaverApi.DTOs.Aircraft;
using FlightSaverApi.Queries.Aircraft;
using MediatR;

namespace FlightSaverApi.Handlers.Airline;

public class GetAirlineQueryHandler : IRequestHandler<GetAircraftQuery, AircraftDTO>
{
    private readonly FlightSaverContext _context;
    private readonly IMapper _mapper;

    public GetAirlineQueryHandler(FlightSaverContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AircraftDTO> Handle(GetAircraftQuery request, CancellationToken cancellationToken)
    {
        var airline = await _context.Airlines.FindAsync(request.Id);

        if (airline == null)
        {
            throw new KeyNotFoundException($"Airline with Id {request.Id} does not exist.");
        }
        
        var airlineDto = _mapper.Map<AircraftDTO>(airline);
        
        return airlineDto;
    }
}