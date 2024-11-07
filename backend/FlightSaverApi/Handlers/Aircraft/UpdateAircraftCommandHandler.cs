using AutoMapper;
using FlightSaverApi.Commands.Aircraft;
using FlightSaverApi.Data;
using FlightSaverApi.Models.AircraftModel;
using MediatR;

namespace FlightSaverApi.Handlers;

public class UpdateAircraftCommandHandler : IRequestHandler<UpdateAircraftCommand, Unit>
{
    private readonly FlightSaverContext _context;
    private readonly IMapper _mapper;

    public UpdateAircraftCommandHandler(FlightSaverContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateAircraftCommand request, CancellationToken cancellationToken)
    {
        var aircraft = await _context.Aircrafts.FindAsync(request.Id);
        if (aircraft == null)
        {
            throw new KeyNotFoundException($"Aircraft with Id {request.Id} does not exist.");
        }
        
        _mapper.Map(request, aircraft);
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}