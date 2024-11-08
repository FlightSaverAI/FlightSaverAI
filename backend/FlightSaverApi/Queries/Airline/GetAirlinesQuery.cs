using FlightSaverApi.Models.AirlineModel;
using MediatR;

namespace FlightSaverApi.Queries.Airline;

public class GetAirlinesQuery : IRequest<IEnumerable<AirlineDTO>>
{
}