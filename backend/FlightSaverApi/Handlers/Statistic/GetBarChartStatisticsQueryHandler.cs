using FlightSaverApi.Interfaces.Services;
using FlightSaverApi.Models;
using FlightSaverApi.Queries.Statistics;
using MediatR;

namespace FlightSaverApi.Handlers.Statistic;

public class GetBarChartStatisticsQueryHandler : IRequestHandler<GetBarChartStatisticsQuery, BarChartStatistics>
{
    private readonly IStatisticsService _statisticsService;

    public GetBarChartStatisticsQueryHandler(IStatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
    }

    public async Task<BarChartStatistics> Handle(GetBarChartStatisticsQuery request, CancellationToken cancellationToken)
    {
        var flightStatistics = await _statisticsService.GetBarChartStatisticsAsync(request.UserId, cancellationToken);
        
        return flightStatistics;
    }
}
