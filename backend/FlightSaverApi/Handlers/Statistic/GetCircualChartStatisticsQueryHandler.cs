using FlightSaverApi.Interfaces.Services;
using FlightSaverApi.Models;
using FlightSaverApi.Queries.Statistics;
using MediatR;

namespace FlightSaverApi.Handlers.Statistic;

public class GetCircualChartStatisticsQueryHandler : IRequestHandler<GetCircualChartStatisticsQuery, CircualChartStatistics>
{
    private readonly IStatisticsService _statisticsService;

    public GetCircualChartStatisticsQueryHandler(IStatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
    }

    public async Task<CircualChartStatistics> Handle(GetCircualChartStatisticsQuery request, CancellationToken cancellationToken)
    {
        var flightStatistics = await _statisticsService.GetCircualChartStatisticsAsync(request.UserId, cancellationToken);
        
        return flightStatistics;
    }
}