using AutoMapper;
using FlightSaverApi.Commands.Aircraft;
using FlightSaverApi.Data;
using FlightSaverApi.Models.AircraftModel;
using FlightSaverApi.Models.AirportModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers;

public class UpdateAircraftCommandHandler : IRequestHandler<UpdateAircraftCommand, AircraftDTO>
{
    private readonly FlightSaverContext _context;
    private readonly IMapper _mapper;

    public UpdateAircraftCommandHandler(FlightSaverContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AircraftDTO> Handle(UpdateAircraftCommand request, CancellationToken cancellationToken)
    {
        var aircraft = await _context.Aircrafts.FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);
        
        if (aircraft == null)
        {
            throw new KeyNotFoundException($"Aircraft with Id {request.Id} does not exist.");
        }
        
        _mapper.Map(request.AircraftDto, aircraft);
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return _mapper.Map<AircraftDTO>(aircraft);;
    }
}