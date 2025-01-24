using AutoMapper;
using FlightSaverApi.Data;
using FlightSaverApi.DTOs.User;
using FlightSaverApi.Interfaces.Services;
using FlightSaverApi.Queries.User;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers.User;

public class GetFriendsQueryHandler : IRequestHandler<GetFriendsQuery, IEnumerable<FriendDTO>>
{
    private readonly FlightSaverContext _context;
    private readonly IMapper _mapper;
    private readonly IStatisticsService _statisticsService;
    
    public GetFriendsQueryHandler(FlightSaverContext context, IMapper mapper, IStatisticsService statisticsService)
    {
        _context = context;
        _mapper = mapper;
        _statisticsService = statisticsService;
    }
    
    public async Task<IEnumerable<FriendDTO>> Handle(GetFriendsQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(u => u.Friends)
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        if (user == null)
        {
            return Enumerable.Empty<FriendDTO>();
        }

        var friendsDTO = _mapper.Map<IEnumerable<FriendDTO>>(user.Friends);

        foreach (var friend in friendsDTO)
        {
            friend.IsLoggedUserFriend = true;
            friend.Statistics = await _statisticsService.GetBasicFlightStatisticsAsync(friend.Id, cancellationToken);
        }

        return friendsDTO;
    }
}