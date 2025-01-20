using FlightSaverApi.Models;
using MediatR;

namespace FlightSaverApi.Queries.Statistics;

public class GetCircualChartStatisticsQuery : IRequest<CircualChartStatistics>
{
    public int UserId { get; set; }

    public GetCircualChartStatisticsQuery(int userId)
    {
        UserId = userId;
    }
}