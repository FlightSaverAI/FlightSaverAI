using FlightSaverApi.Enums;
using FlightSaverApi.Enums.FlightEnums;
using FlightSaverApi.Models;

namespace FlightSaverApi.Interfaces.Services;

public interface IStatisticsService
{
    Task<FlightStatistics> GetFlightStatisticsAsync(int userId, CancellationToken cancellationToken = default);
    Task<CircualChartStatistics> GetCircualChartStatisticsAsync(int userId, CancellationToken cancellationToken = default);
    Task<BarChartStatistics> GetBarChartStatisticsAsync(int userId, CancellationToken cancellationToken = default);

    Task<LineChartStatistics> GetLineChartStatisticsAsync(int userId, CancellationToken cancellationToken = default);
    Task<BasicFlightStatistics> GetBasicFlightStatisticsAsync(int userId, CancellationToken cancellationToken = default);
    Dictionary<ClassType, int> GetClassDistribution(List<Flight> flights);
    List<CircualData> GetCircualDataClassDistribution(List<Flight> flights);
    Dictionary<SeatType, int> GetSeatDistribution(List<Flight> flights);
    List<CircualData> GetCircualDataSeatDistribution(List<Flight> flights);
    Dictionary<Reason, int> GetReasonDistribution(List<Flight> flights);
    List<CircualData> GetCircualDataReasonDistribution(List<Flight> flights);
    FlightCount GetFlightCount(List<Flight> flights);
    Task<Dictionary<Continent, int>> GetContinentsAsync(List<Flight> flights);
    Task<List<CircualData>> GetCircualDataContinentsAsync(List<Flight> flights);
    Dictionary<string, int> GetTopAirports(List<Flight> flights);
    Dictionary<string, int> GetTopAirlines(List<Flight> flights);
    Dictionary<string, int> GetTopAircraft(List<Flight> flights);
    Dictionary<string, int> GetTopFlightRoutes(List<Flight> flights);
    Dictionary<Month, int> GetFlightsPerMonth(List<Flight> flights, int? year = null);
    Dictionary<DayOfWeek, int> GetFlightsPerWeek(List<Flight> flights, int? weekNumber = null);
    Distance GetDistance(List<Flight> flights);
    FlightTime GetTotalFlightTime(List<Flight> flights);
}