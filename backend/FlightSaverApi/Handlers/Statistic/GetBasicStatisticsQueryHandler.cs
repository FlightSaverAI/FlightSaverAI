using FlightSaverApi.Interfaces.Services;
using FlightSaverApi.Models;
using FlightSaverApi.Queries.Statistics;
using MediatR;

namespace FlightSaverApi.Handlers.Statistic;

public class GetBasicStatisticsQueryHandler : IRequestHandler<GetBasicStatisticsQuery, BasicFlightStatistics>
{
    private readonly IStatisticsService _statisticsService;

    public GetBasicStatisticsQueryHandler(IStatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
    }

    public async Task<BasicFlightStatistics> Handle(GetBasicStatisticsQuery request,
        CancellationToken cancellationToken)
    {
        var basicFlightStatistics = await _statisticsService.GetBasicFlightStatisticsAsync(request.UserId);
        
        return basicFlightStatistics;
    }
}