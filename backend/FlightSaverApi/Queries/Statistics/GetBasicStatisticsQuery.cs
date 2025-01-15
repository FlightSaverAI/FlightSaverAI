using FlightSaverApi.Models;
using MediatR;

namespace FlightSaverApi.Queries.Statistics;

public class GetBasicStatisticsQuery : IRequest<BasicFlightStatistics>
{
    public int UserId { get; set; }

    public GetBasicStatisticsQuery(int userId)
    {
        UserId = userId;
    }
}