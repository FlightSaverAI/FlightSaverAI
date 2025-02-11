using AutoMapper;
using FlightSaverApi.Data;
using FlightSaverApi.DTOs.User;
using FlightSaverApi.Enums;
using FlightSaverApi.Queries.User;
using FlightSaverApi.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers.User;

public class GetFriendsBasicQueryHandler : IRequestHandler<GetFriendsBasicQuery, IEnumerable<FriendBasicDTO>>
{
    private readonly FlightSaverContext _context;
    private readonly IMapper _mapper;
    
    public GetFriendsBasicQueryHandler(FlightSaverContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<FriendBasicDTO>> Handle(GetFriendsBasicQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(u => u.Friends)
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        if (user == null)
        {
            return new List<FriendBasicDTO>();
        }

        var friendsQuery = _context.Users
            .Where(u => u.Id == request.UserId)
            .SelectMany(u => u.Friends)
            .AsQueryable();
    
        if (request.UserRole != UserRole.Admin)
        {
            friendsQuery = friendsQuery.Where(u => u.Role != UserRole.Admin);
        }

        var friendsDTO = _mapper.Map<IEnumerable<FriendBasicDTO>>(await friendsQuery.ToListAsync(cancellationToken));

        return friendsDTO;
    }
}