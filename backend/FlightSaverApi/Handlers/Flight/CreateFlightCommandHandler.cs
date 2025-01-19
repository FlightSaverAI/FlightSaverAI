using AutoMapper;
using FlightSaverApi.Commands.Flight;
using FlightSaverApi.Data;
using FlightSaverApi.DTOs.Aircraft;
using FlightSaverApi.DTOs.Flight;
using MediatR;

namespace FlightSaverApi.Handlers.Flight;

public class CreateFlightCommandHandler : IRequestHandler<CreateFlightCommand, NewFlightDTO>
{
    public readonly FlightSaverContext _context;
    public readonly IMapper _mapper;

    public CreateFlightCommandHandler(FlightSaverContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<NewFlightDTO> Handle(CreateFlightCommand request, CancellationToken cancellationToken)
    {
        var flight = _mapper.Map<Models.Flight>(request.NewFlightDTO);
        
        if(flight.ArrivalTime.Hour <= flight.DepartureTime.Hour)
            flight.ArrivalTime = flight.DepartureTime.AddDays(1);
        
        if(flight.AirportReviews?.Any() == true)
            foreach (var review in flight.AirportReviews)
            {
                review.FlightId = flight.Id;
                review.UserId = request.NewFlightDTO.UserId ?? 0;
            }

        if (flight.AircraftReview != null)
        {
            flight.AircraftReview.FlightId = flight.Id;
            flight.AircraftReview.UserId = request.NewFlightDTO.UserId ?? 0;
        }

        if (flight.AirlineReview != null)
        {
            flight.AirlineReview.FlightId = flight.Id;
            flight.AirlineReview.UserId = request.NewFlightDTO.UserId ?? 0;
        }
            
        
        _context.Flights.Add(flight);
        await _context.SaveChangesAsync(cancellationToken);
        
        return _mapper.Map<NewFlightDTO>(flight);
    }
}