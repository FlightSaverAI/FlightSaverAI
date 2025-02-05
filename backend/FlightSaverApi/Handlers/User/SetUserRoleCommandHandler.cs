using AutoMapper;
using FlightSaverApi.Commands.User;
using FlightSaverApi.Data;
using FlightSaverApi.Enums;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers.User;

public class SetUserRoleCommandHandler : IRequestHandler<SetUserRoleCommand, Unit>
{
    private readonly FlightSaverContext _context;
    private readonly IMapper _mapper;

    public SetUserRoleCommandHandler(FlightSaverContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
        
    public async Task<Unit> Handle(SetUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(a => a.Id == request.UpdateUserRoleDto.Id, cancellationToken);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with Id {request.UpdateUserRoleDto.Id} does not exist.");
        }
        
        if (!Enum.IsDefined(typeof(UserRole), request.UpdateUserRoleDto.Role))
        {
            throw new ArgumentException($"Invalid role: {request.UpdateUserRoleDto.Role}");
        }
        
        _mapper.Map(request.UpdateUserRoleDto, user);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}