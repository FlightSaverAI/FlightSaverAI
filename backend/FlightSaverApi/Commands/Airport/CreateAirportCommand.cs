using FlightSaverApi.Models.AirportModel;
using MediatR;

namespace FlightSaverApi.Commands.Airport;

public class CreateAirportCommand : IRequest<AirportDTO>
{
    public AirportDTO AirportDto { get; set; }  
}