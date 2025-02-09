using FlightSaverApi.Commands.User;
using FlightSaverApi.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers.User;

public class RemoveFriendCommandHandler : IRequestHandler<RemoveFriendCommand, Unit>
{
    private readonly FlightSaverContext _context;

    public RemoveFriendCommandHandler(FlightSaverContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(RemoveFriendCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(u => u.Friends)
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        var friend = await _context.Users
            .Include(u => u.Friends)    
            .FirstOrDefaultAsync(u => u.Id == request.FriendId, cancellationToken);

        if (user == null || friend == null)
            throw new KeyNotFoundException("User or friend not found.");

        if (!user.Friends.Contains(friend))
            throw new InvalidOperationException("This user is not your friend.");

        user.Friends.Remove(friend);
        friend.Friends.Remove(user);
        
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}