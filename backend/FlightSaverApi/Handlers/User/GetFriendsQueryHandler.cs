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
    
    public async Task<IEnumerable<FriendDTO>> Handle(GetFriendsQuery request,
        CancellationToken cancellationToken)
    {
        var users = await _context.Users.Where(x => x.Id != request.UserId).ToListAsync(cancellationToken);
        
        var usersDTO = _mapper.Map<IEnumerable<FriendDTO>>(users);

        if (usersDTO != null || users.Count == 0)
        {
            foreach (var user in usersDTO)
            {
                user.Statistics = await _statisticsService.GetBasicFlightStatisticsAsync(user.Id, cancellationToken);
            }
        }
        
        return usersDTO;
    }
}