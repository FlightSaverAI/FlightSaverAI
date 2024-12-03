using FlightSaverApi.Enums;
using FlightSaverApi.Enums.FlightEnums;
using FlightSaverApi.Models.AircraftModel;
using FlightSaverApi.Models.AirlineModel;
using FlightSaverApi.Models.AirportModel;
using FlightSaverApi.Models.FlightModel;
using FlightSaverApi.Models.StatisticsModel;

namespace FlightSaverApi.Interfaces.Services;

public interface IStatisticsService
{
    Task<FlightStatistics> GetFlightStatisticsAsync(int userId, CancellationToken cancellationToken = default);
    Dictionary<ClassType, int> GetClassDistributionAsync(List<Flight> flights);
    Dictionary<SeatType, int> GetSeatDistributionAsync(List<Flight> flights);
    Dictionary<Reason, int> GetReasonDistributionAsync(List<Flight> flights);
    Dictionary<FlightType, int> GetFlightDistributionAsync(List<Flight> flights);
    Task<Dictionary<Continent, int>> GetContinentsAsync(List<Flight> flights);
    Dictionary<string, int> GetTopAirportsAsync(List<Flight> flights);
    Dictionary<string, int> GetTopAirlinesAsync(List<Flight> flights);
    Dictionary<string, int> GetTopAircraftAsync(List<Flight> flights);
    Dictionary<string, int> GetTopFlightRoutesAsync(List<Flight> flights);
    Dictionary<Month, int> GetFlightsPerMonthAsync(List<Flight> flights, int? year = null);
    Dictionary<DayOfWeek, int> GetFlightsPerWeekAsync(List<Flight> flights, int? weekNumber = null);
    Distance GetDistanceAsync(List<Flight> flights);
    TimeSpan GetTotalFlightTimeAsync(List<Flight> flights);
}