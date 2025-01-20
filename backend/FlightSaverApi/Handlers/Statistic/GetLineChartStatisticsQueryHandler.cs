using FlightSaverApi.Interfaces.Services;
using FlightSaverApi.Models;
using FlightSaverApi.Queries.Statistics;
using MediatR;
using NuGet.Protocol.Plugins;

namespace FlightSaverApi.Handlers.Statistic;

public class GetLineChartStatisticsQueryHandler : IRequestHandler<GetLineChartStatisticsQuery, LineChartStatistics>
{
    private readonly IStatisticsService _statisticsService;

    public GetLineChartStatisticsQueryHandler(IStatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
    }

    public async Task<LineChartStatistics> Handle(GetLineChartStatisticsQuery request, CancellationToken cancellationToken)
    {
        var flightStatistics = await _statisticsService.GetLineChartStatisticsAsync(request.UserId, cancellationToken);
        
        return flightStatistics;
    }
}