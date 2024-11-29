using AutoMapper;
using FlightSaverApi.Commands.Airline;
using FlightSaverApi.Models.AirlineModel;
using FlightSaverApi.Data;
using MediatR;
using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace FlightSaverApi.Handlers.Airline;

public class CreateAirlineCommandHandler : IRequestHandler<CreateAirlineCommand, AirlineDTO>
{
    public readonly FlightSaverContext _context;
    public readonly IMapper _mapper;

    public CreateAirlineCommandHandler(FlightSaverContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AirlineDTO> Handle(CreateAirlineCommand request, CancellationToken cancellationToken)
    {
        var airline = _mapper.Map<Models.AirlineModel.Airline>(request.AirlineDto);

        _context.Airlines.Add(airline);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<AirlineDTO>(airline);
    }
}