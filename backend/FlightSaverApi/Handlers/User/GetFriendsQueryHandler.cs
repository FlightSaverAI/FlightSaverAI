using AutoMapper;
using FlightSaverApi.Data;
using FlightSaverApi.DTOs.User;
using FlightSaverApi.Interfaces.Services;
using FlightSaverApi.Queries.User;
using FlightSaverApi.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers.User;

public class GetFriendsQueryHandler : IRequestHandler<GetFriendsQuery, PagedUserResult>
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
    
    public async Task<PagedUserResult> Handle(GetFriendsQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(u => u.Friends)
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        if (user == null)
        {
            return new PagedUserResult();
        }

        var friendsDTO = _mapper.Map<IEnumerable<FriendDTO>>(user.Friends);

        foreach (var friend in friendsDTO)
        {
            friend.IsLoggedUserFriend = true;
            friend.Statistics = await _statisticsService.GetBasicFlightStatisticsAsync(friend.Id, cancellationToken);
        }
        
        var totalRecords = 0;
        var totalPages = 0;
        
        if (request.PageNumber.HasValue && request.PageSize.HasValue && request.PageNumber > 0 && request.PageSize > 0)
        {
            totalRecords = friendsDTO.Count();
            totalPages = (int)Math.Ceiling((double)totalRecords / (request.PageSize ?? 10));
            return new PagedUserResult()
            {
                Users = friendsDTO.Skip((request.PageNumber.Value - 1) * request.PageSize.Value)
                    .Take(request.PageSize.Value)
                    .ToList(),
                TotalPages = totalPages
            };
        }

        return new PagedUserResult()
        {
            Users = friendsDTO,
            TotalPages = totalPages
        };
    }
}