using AutoMapper;
using FlightSaverApi.Commands.Airport;
using FlightSaverApi.Data;
using FlightSaverApi.DTOs;
using FlightSaverApi.DTOs.Airport;
using MediatR;

namespace FlightSaverApi.Handlers.Airport;

public class CreateAirportCommandHandler : IRequestHandler<CreateAirportCommand, AirportDTO>
{
    public readonly FlightSaverContext _context;
    public readonly IMapper _mapper;

    public CreateAirportCommandHandler(FlightSaverContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AirportDTO> Handle(CreateAirportCommand request, CancellationToken cancellationToken)
    {
        var airport = _mapper.Map<Models.Airport>(request);
        
        _context.Airports.Add(airport);
        await _context.SaveChangesAsync(cancellationToken);
        
        return _mapper.Map<AirportDTO>(airport);
    }
}