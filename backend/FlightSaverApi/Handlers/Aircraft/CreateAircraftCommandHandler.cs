using System.CodeDom;
using AutoMapper;
using FlightSaverApi.Commands.Aircraft;
using FlightSaverApi.Data;
using FlightSaverApi.DTOs;
using FlightSaverApi.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlightSaverApi.Handlers;

public class CreateAircraftCommandHandler : IRequestHandler<CreateAircraftCommand, AircraftDTO>
{
    private readonly FlightSaverContext _context;
    private readonly IMapper _mapper;

    public CreateAircraftCommandHandler(FlightSaverContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AircraftDTO> Handle(CreateAircraftCommand request, CancellationToken cancellationToken)
    {
        var aircraft = new Aircraft
        {
            Name = request.Name,
            IataCode = request.IataCode,
            IcaoCode = request.IcaoCode,
            RegNumber = request.RegNumber,
            AircraftUrl = request.AircraftUrl,
            AirlineId = request.AirlineId
        };
        
        _context.Aircrafts.Add(aircraft);
        await _context.SaveChangesAsync(cancellationToken);

        var aircraftDto = _mapper.Map<AircraftDTO>(aircraft);
        
        return aircraftDto;
    }
}