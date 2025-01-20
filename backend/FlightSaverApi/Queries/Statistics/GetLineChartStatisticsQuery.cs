using FlightSaverApi.Models;
using MediatR;

namespace FlightSaverApi.Queries.Statistics;

public class GetLineChartStatisticsQuery : IRequest<LineChartStatistics>
{
    public int UserId { get; set; }

    public GetLineChartStatisticsQuery(int userId)
    {
        UserId = userId;
    }
}