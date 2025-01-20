using FlightSaverApi.Enums;
using FlightSaverApi.Enums.FlightEnums;
using Newtonsoft.Json;

namespace FlightSaverApi.Models;

public class FlightStatistics
{
    public Dictionary<ClassType, int> ClassDistribution { get; set; }
    public Dictionary<SeatType, int> SeatDistribution { get; set; }
    public Dictionary<Reason, int> ReasonDistribution { get; set; }
    public Dictionary<Continent, int> Continents { get; set; }
    public Dictionary<string, int> TopAirports { get; set; }
    public Dictionary<string, int> TopAirlines { get; set; }
    public Dictionary<string, int> TopAircrafts { get; set; }
    public Dictionary<string, int> FlightRoutes { get; set; }
    public Dictionary<Month, int> FlightsPerMonth { get; set; }
    public Dictionary<DayOfWeek, int> FlightsPerWeek { get; set; }
}

public class CircualChartStatistics
{
    public List<CircualData> ClassDistribution { get; set; }
    public List<CircualData> SeatDistribution { get; set; }
    public List<CircualData> ReasonDistribution { get; set; }
    public List<CircualData> Continents { get; set; }
}

public class CircualData
{
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("value")]
    public int Value { get; set; }
}

public class BarChartStatistics
{
    public Dictionary<string, int> TopAirports { get; set; }
    public Dictionary<string, int> TopAirlines { get; set; }
    public Dictionary<string, int> TopAircrafts { get; set; }
    public Dictionary<string, int> FlightRoutes { get; set; }
}

public class LineChartStatistics
{
    public Dictionary<Month, int> FlightsPerMonth { get; set; }
    public Dictionary<DayOfWeek, int> FlightsPerWeek { get; set; }
}

public class BasicFlightStatistics
{
    public FlightCount FlightCount { get; set; }
    public Distance Distance { get; set; } = new Distance();
    public FlightTime TotalFlightTime { get; set; }
}

public class FlightCount
{
    public int Count { get; set; }
    public Dictionary<FlightType, int> FlightDistribution { get; set; }
}

public class FlightTime
{
    public TimeSpan Time { get; set; }
    public double Days => Time.TotalDays;
    public double Months => Time.TotalDays / 30.44;
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
        return $"{From.IataCode} - {To.IataCode}";
    }

    public override bool Equals(object obj)
    {
        if (obj is not FlightRoute other) return false;
        return From.Id == other.From.Id && To.Id == other.To.Id;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(From.Id, To.Id);
    }
}

public class Distance
{
    public double TotalDistance { get; set; }
    public double AroundEarthDistance => TotalDistance / Constants.EarthCircumferenceKm;
    public double ToTheMoonDistance => TotalDistance / Constants.MoonDistanceKm;
}