using FlightSaverApi.Commands.User;
using FlightSaverApi.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers.User;

public class AddFriendCommandHandler : IRequestHandler<AddFriendCommand, Unit>
{
    private readonly FlightSaverContext _context;

    public AddFriendCommandHandler(FlightSaverContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(AddFriendCommand request, CancellationToken cancellationToken)
    {
        if (request.UserId == request.FriendId)
            throw new InvalidOperationException("You cannot add yourself as a friend.");

        var user = await _context.Users
            .Include(u => u.Friends)
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        var friend = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == request.FriendId, cancellationToken);

        if (user == null || friend == null)
            throw new KeyNotFoundException("User or friend not found.");

        if (user.Friends.Contains(friend))
            throw new InvalidOperationException("This user is already your friend.");

        user.Friends.Add(friend);
        friend.Friends.Add(user);
        
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}