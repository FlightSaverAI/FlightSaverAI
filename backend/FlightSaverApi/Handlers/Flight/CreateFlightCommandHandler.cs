using AutoMapper;
using FlightSaverApi.Commands.Flight;
using FlightSaverApi.Data;
using FlightSaverApi.Models.FlightModel;
using MediatR;

namespace FlightSaverApi.Handlers.Flight;

public class CreateFlightCommandHandler : IRequestHandler<CreateFlightCommand, FlightDTO>
{
    public readonly FlightSaverContext _context;
    public readonly IMapper _mapper;

    public CreateFlightCommandHandler(FlightSaverContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<FlightDTO> Handle(CreateFlightCommand request, CancellationToken cancellationToken)
    {
        var flight = _mapper.Map<Models.FlightModel.Flight>(request);
        
        _context.Flights.Add(flight);
        await _context.SaveChangesAsync();
        
        return _mapper.Map<FlightDTO>(flight);
    }
}