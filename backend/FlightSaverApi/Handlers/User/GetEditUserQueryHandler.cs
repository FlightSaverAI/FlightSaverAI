using AutoMapper;
using FlightSaverApi.Data;
using FlightSaverApi.DTOs.Aircraft;
using FlightSaverApi.DTOs.User;
using FlightSaverApi.Queries.User;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers.User;

public class GetEditUserQueryHandler : IRequestHandler<GetEditUserQuery, EditUserDTO>
{
    private readonly FlightSaverContext _context;
    private readonly IMapper _mapper;
    
    public GetEditUserQueryHandler(FlightSaverContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<EditUserDTO> Handle(GetEditUserQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(request.Id, cancellationToken);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with Id {request.Id} does not exist.");
        }
        
        var userDto = _mapper.Map<EditUserDTO>(user);
        
        return userDto;
    }
}