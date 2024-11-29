using AutoMapper;
using FlightSaverApi.Data;
using FlightSaverApi.Interfaces.Services;
using FlightSaverApi.Models.StatisticsModel;
using FlightSaverApi.Queries.Statistics;
using MediatR;

namespace FlightSaverApi.Handlers.Statistic;

public class GetStatisticsQueryHandler : IRequestHandler<GetStatisticsQuery, FlightStatistics>
{
    private readonly IStatisticsService _statisticsService;

    public GetStatisticsQueryHandler(IStatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
    }

    public async Task<FlightStatistics> Handle(GetStatisticsQuery request, CancellationToken cancellationToken)
    {
        var flightStatistics = await _statisticsService.GetFlightStatisticsAsync(request.UserId, cancellationToken);
        
        return flightStatistics;
    }
}