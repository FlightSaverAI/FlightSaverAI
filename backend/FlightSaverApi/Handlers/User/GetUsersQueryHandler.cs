using AutoMapper;
using FlightSaverApi.Data;
using FlightSaverApi.DTOs.User;
using FlightSaverApi.Interfaces.Services;
using FlightSaverApi.Queries.User;
using FlightSaverApi.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers.User;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PagedUserResult>
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
    
    public async Task<PagedUserResult> Handle(GetUsersQuery request, CancellationToken cancellationToken)
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
            return new PagedUserResult();
        }

        var loggedUserFriends = loggedUser.Friends.Select(f => f.Id).ToHashSet();

        var usersDTO = _mapper.Map<IEnumerable<FriendDTO>>(users);

        foreach (var user in usersDTO)
        {
            user.IsLoggedUserFriend = loggedUserFriends.Contains(user.Id);
            user.Statistics = await _statisticsService.GetBasicFlightStatisticsAsync(user.Id, cancellationToken);
        }

        var totalRecords = 0;
        var totalPages = 0;
        
        if (request.PageNumber.HasValue && request.PageSize.HasValue && request.PageNumber > 0 && request.PageSize > 0)
        {
            totalRecords = usersDTO.Count();
            totalPages = (int)Math.Ceiling((double)totalRecords / (request.PageSize ?? 10));
            return new PagedUserResult()
            {
                Users = usersDTO.Skip((request.PageNumber.Value - 1) * request.PageSize.Value)
                    .Take(request.PageSize.Value)
                    .ToList(),
                TotalPages = totalPages
            };
        }
        
        totalPages = (int)Math.Ceiling((double)totalRecords / (request.PageSize ?? 10));
            
        return new PagedUserResult
        {
            Users = usersDTO,
            TotalPages = totalPages,
        };
    }
}