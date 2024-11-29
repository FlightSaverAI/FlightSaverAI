using FlightSaverApi.Models.StatisticsModel;
using MediatR;

namespace FlightSaverApi.Queries.Statistics;

public class GetStatisticsQuery : IRequest<FlightStatistics>
{
    public int UserId { get; set; }

    public GetStatisticsQuery(int userId)
    {
        UserId = userId;
    }
}