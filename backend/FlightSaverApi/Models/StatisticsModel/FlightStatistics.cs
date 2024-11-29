using System.Runtime.InteropServices.JavaScript;
using FlightSaverApi.Enums;
using FlightSaverApi.Enums.FlightEnums;
using FlightSaverApi.Models.AircraftModel;
using FlightSaverApi.Models.AirlineModel;
using FlightSaverApi.Models.AirportModel;

namespace FlightSaverApi.Models.StatisticsModel;

public class FlightStatistics
{
    public Dictionary<ClassType, int> ClassDistribution { get; set; }
    public Dictionary<SeatType, int> SeatDistribution { get; set; }
    public Dictionary<Reason, int> ReasonDistribution { get; set; }
    public Dictionary<Continent, int> Continents { get; set; }
    public Dictionary<Airport, int> TopAirports { get; set; }
    public Dictionary<Airline, int> TopAirlines { get; set; }
    public Dictionary<Aircraft, int> TopAircrafts { get; set; }
    public Dictionary<FlightRoute, int> FlightRoutes { get; set; }
    public Dictionary<Month, int> FlightsPerMonth { get; set; }
    public Dictionary<DayOfWeek, int> FlightsPerWeek { get; set; }
}

public class FlightRoute
{
    public Airport From { get; set; }
    public Airport To { get; set; }

    public FlightRoute(Airport from, Airport to)
    {
        From = from;
        To = to;
    }

    public override string ToString()
    {
        return $"{From} - {To}";
    }
}