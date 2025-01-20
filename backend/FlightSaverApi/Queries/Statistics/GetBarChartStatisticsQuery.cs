using FlightSaverApi.Models;
using MediatR;

namespace FlightSaverApi.Queries.Statistics;

public class GetBarChartStatisticsQuery : IRequest<BarChartStatistics>
{
    public int UserId { get; set; }

    public GetBarChartStatisticsQuery(int userId)
    {
        UserId = userId;
    }
}