using AutoMapper;
using FlightSaverApi.Commands.Flight;
using FlightSaverApi.Data;
using FlightSaverApi.Models.FlightModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Handlers.Flight;

public class UpdateFlightCommandHandler : IRequestHandler<UpdateFlightCommand, FlightDTO>
{
    private readonly FlightSaverContext _context;
    private readonly IMapper _mapper;

    public UpdateFlightCommandHandler(FlightSaverContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<FlightDTO> Handle(UpdateFlightCommand request, CancellationToken cancellationToken)
    {
        var flight = await _context.Flights
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

        if (flight == null)
        {
            throw new KeyNotFoundException($"Flight with Id {request.Id} does not exist.");
        }

        _mapper.Map(request.FlightDto, flight);

        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<FlightDTO>(flight);
    }
}