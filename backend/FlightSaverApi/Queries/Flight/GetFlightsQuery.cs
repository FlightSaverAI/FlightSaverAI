using FlightSaverApi.Models.FlightModel;
using MediatR;

namespace FlightSaverApi.Queries.Flight;

public class GetFlightsQuery : IRequest<IEnumerable<FlightDTO>>
{
}