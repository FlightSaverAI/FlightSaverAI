using AutoMapper;
using FlightSaverApi.Data;
using FlightSaverApi.DTOs.Aircraft;
using FlightSaverApi.DTOs.User;
using FlightSaverApi.Queries.User;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers.User;

public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, UserInfoDTO>
{
    private readonly FlightSaverContext _context;
    private readonly IMapper _mapper;
    
    public GetUserInfoQueryHandler(FlightSaverContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<UserInfoDTO> Handle(GetUserInfoQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(request.Id, cancellationToken);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with Id {request.Id} does not exist.");
        }
        
        var userDto = _mapper.Map<UserInfoDTO>(user);
        
        return userDto;
    }
}