using AutoMapper;
using FlightSaverApi.Commands.Airport;
using FlightSaverApi.Data;
using FlightSaverApi.DTOs;
using FlightSaverApi.DTOs.Airport;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers.Airport;

public class UpdateAirportCommandHandler : IRequestHandler<UpdateAirportCommand, AirportDTO>
{
    private readonly FlightSaverContext _context;
    private readonly IMapper _mapper;

    public UpdateAirportCommandHandler(FlightSaverContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AirportDTO> Handle(UpdateAirportCommand request, CancellationToken cancellationToken)
    {
        var airport = await _context.Airports
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

        if (airport == null)
        {
            throw new KeyNotFoundException($"Airport with Id {request.Id} does not exist.");
        }

        _mapper.Map(request.AirportDto, airport);

        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<AirportDTO>(airport);
    }
}