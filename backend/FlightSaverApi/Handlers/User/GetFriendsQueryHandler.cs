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

    var friendsQuery = _context.Users
        .Where(u => u.Id == request.UserId)
        .SelectMany(u => u.Friends)
        .AsQueryable();

    if (!string.IsNullOrEmpty(request.Name))
    {
        friendsQuery = friendsQuery.Where(f => EF.Functions.Like(f.Username.ToLower(), $"%{request.Name.ToLower()}%"));
    }

    var friendsDTO = _mapper.Map<IEnumerable<FriendDTO>>(await friendsQuery.ToListAsync(cancellationToken));

    foreach (var friend in friendsDTO)
    {
        friend.IsLoggedUserFriend = true;
        friend.Statistics = await _statisticsService.GetBasicFlightStatisticsAsync(friend.Id, cancellationToken);
    }

    var totalRecords = friendsDTO.Count();
    
    var pageNumber = request.PageNumber ?? 1;
    var pageSize = request.PageSize ?? 10;

    var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

    return new PagedUserResult()
    {
        Users = friendsDTO.Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList(),
        TotalPages = totalPages
    };
}

}