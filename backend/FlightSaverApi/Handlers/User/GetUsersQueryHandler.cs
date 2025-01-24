using AutoMapper;
using FlightSaverApi.Data;
using FlightSaverApi.DTOs.User;
using FlightSaverApi.Interfaces.Services;
using FlightSaverApi.Queries.User;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers.User;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<FriendDTO>>
{
    private readonly FlightSaverContext _context;
    private readonly IMapper _mapper;
    private readonly IStatisticsService _statisticsService;
    
    public GetUsersQueryHandler(FlightSaverContext context, IMapper mapper, IStatisticsService statisticsService)
    {
        _context = context;
        _mapper = mapper;
        _statisticsService = statisticsService;
    }
    
    public async Task<IEnumerable<FriendDTO>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _context.Users
            .Include(u => u.Friends)
            .Where(x => x.Id != request.UserId)
            .ToListAsync(cancellationToken);

        var loggedUser = await _context.Users
            .Include(u => u.Friends)
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        if (loggedUser == null)
        {
            return Enumerable.Empty<FriendDTO>();
        }

        var loggedUserFriends = loggedUser.Friends.Select(f => f.Id).ToHashSet();

        var usersDTO = _mapper.Map<IEnumerable<FriendDTO>>(users);

        foreach (var user in usersDTO)
        {
            user.IsLoggedUserFriend = loggedUserFriends.Contains(user.Id);
            user.Statistics = await _statisticsService.GetBasicFlightStatisticsAsync(user.Id, cancellationToken);
        }
            
        return usersDTO;
    }
}