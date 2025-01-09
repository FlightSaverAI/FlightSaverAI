using AutoMapper;
using FlightSaverApi.Commands.Flight;
using FlightSaverApi.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers.Flight;

public class DeleteFlightCommandHandler : IRequestHandler<DeleteFlightCommand, bool>
{
    public readonly FlightSaverContext _context;
    public readonly IMapper _mapper;

    public DeleteFlightCommandHandler(FlightSaverContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> Handle(DeleteFlightCommand request, CancellationToken cancellationToken)
    {
        var flight = await _context.Flights.FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);
        
        if (flight == null)
        {
            return false;
        }
        
        _context.Flights.Remove(flight);
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}