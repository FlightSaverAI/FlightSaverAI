using AutoMapper;
using FlightSaverApi.Data;
using FlightSaverApi.DTOs.User;
using FlightSaverApi.Interfaces.Services;
using FlightSaverApi.Models;
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
        var usersQuery = _context.Users
            .Include(u => u.Friends)
            .Where(x => x.Id != request.UserId);

        if (!string.IsNullOrEmpty(request.Name))
        {
            usersQuery = usersQuery.Where(u => EF.Functions.Like(u.Username.ToLower(), $"%{request.Name.ToLower()}%"));
        }
        else
        {
            return new PagedUserResult();
        }

        var users = await usersQuery.ToListAsync(cancellationToken);

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

        var totalRecords = usersDTO.Count();
        
        var pageNumber = request.PageNumber ?? 1;
        var pageSize = request.PageSize ?? 10;

        var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

        return new PagedUserResult()
        {
            Users = usersDTO.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList(),
            TotalPages = totalPages
        };
    }
}