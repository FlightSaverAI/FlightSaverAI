using AutoMapper;
using FlightSaverApi.Data;
using FlightSaverApi.DTOs;
using FlightSaverApi.Queries.Aircraft;
using MediatR;

namespace FlightSaverApi.Handlers;

public class GetAircraftQueryHandler : IRequestHandler<GetAircraftQuery, AircraftDTO>
{
    private readonly FlightSaverContext _context;
    private readonly IMapper _mapper;

    public GetAircraftQueryHandler(FlightSaverContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AircraftDTO> Handle(GetAircraftQuery request, CancellationToken cancellationToken)
    {
        var aircraft = await _context.Aircrafts.FindAsync(request.Id, cancellationToken);
        if (aircraft == null)
        {
            throw new KeyNotFoundException($"Aircraft with Id {request.Id} does not exist.");
        }
        
        var aircraftDto = _mapper.Map<AircraftDTO>(aircraft);
        
        return aircraftDto;
    }
}