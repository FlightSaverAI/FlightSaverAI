using MediatR;

namespace FlightSaverApi.Commands.User;

public class RemoveFriendCommand : IRequest<Unit>
{
    public int UserId { get; set; }
    public int FriendId { get; set; }
}