using FlightSaverApi.Commands.Aircraft;
using FlightSaverApi.Data;
using MediatR;

namespace FlightSaverApi.Handlers;

public class DeleteAircraftCommandHandler : IRequestHandler<DeleteAircraftCommand, Unit>
{
    private readonly  FlightSaverContext _context;

    public DeleteAircraftCommandHandler(FlightSaverContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteAircraftCommand request, CancellationToken cancellationToken)
    {
        var aircraft = await _context.Aircrafts.FindAsync(request.Id);
        if (aircraft == null)
        {
            throw new KeyNotFoundException($"Aircraft with Id {request.Id} does not exist.");
        }
        
        _context.Aircrafts.Remove(aircraft);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}