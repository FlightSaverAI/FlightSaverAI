using AutoMapper;
using FlightSaverApi.Commands.User;
using FlightSaverApi.Data;
using FlightSaverApi.DTOs.User;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers.User;

public class UpdateBasicInfoCommandHandler : IRequestHandler<UpdateBasicInfoCommand, EditedUserDTO>
{
    private readonly FlightSaverContext _context;
    private readonly IMapper _mapper;

    public UpdateBasicInfoCommandHandler(FlightSaverContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
        
    public async Task<EditedUserDTO> Handle(UpdateBasicInfoCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(a => a.Id == request.UserId, cancellationToken);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with Id {request.UserId} does not exist.");
        }
        
        _mapper.Map(request.UpdateBasicInfoDto, user);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<EditedUserDTO>(user);
    }
}