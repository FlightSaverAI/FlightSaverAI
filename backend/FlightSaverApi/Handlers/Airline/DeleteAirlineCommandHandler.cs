using FlightSaverApi.Commands.Airline;
using FlightSaverApi.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers.Airline;

public class DeleteAirlineCommandHandler : IRequestHandler<DeleteAirlineCommand, bool>
{
    public readonly FlightSaverContext _context;

    public DeleteAirlineCommandHandler(FlightSaverContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteAirlineCommand request, CancellationToken cancellationToken)
    {
        var airline = await _context.Airlines
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

        if (airline == null)
        {
            return false;
        }

        _context.Airlines.Remove(airline);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}