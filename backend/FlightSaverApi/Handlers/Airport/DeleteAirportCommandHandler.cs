using AutoMapper;
using FlightSaverApi.Commands.Airport;
using FlightSaverApi.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers.Airport;

public class DeleteAirportCommandHandler : IRequestHandler<DeleteAirportCommand, bool>
{
    public readonly FlightSaverContext _context;
    public readonly IMapper _mapper;

    public DeleteAirportCommandHandler(FlightSaverContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> Handle(DeleteAirportCommand request, CancellationToken cancellationToken)
    {
        var airport = await _context.Airports.FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);
        
        if (airport == null)
        {
            return false;
        }
        
        _context.Airports.Remove(airport);
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}