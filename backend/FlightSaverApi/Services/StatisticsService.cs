using FlightSaverApi.Enums;
using FlightSaverApi.Enums.FlightEnums;
using FlightSaverApi.Helpers;
using FlightSaverApi.Interfaces.Services;
using FlightSaverApi.Models;

namespace FlightSaverApi.Services;

public class StatisticsService : IStatisticsService
{
    private readonly IFlightsService _flightsService;
    private readonly CountryContinentService _countryContinentService;
    
    public StatisticsService(IFlightsService flightsService, CountryContinentService countryContinentService)
    {
        _flightsService = flightsService;
        _countryContinentService = countryContinentService;
    }
    
    public async Task<FlightStatistics> GetFlightStatisticsAsync(int userId, CancellationToken cancellationToken = default)
    {
        var flights = await _flightsService.GetFlightsByUserIdAsync(userId, cancellationToken);
        
        if (flights.Count == 0) return new FlightStatistics();
        
        var flightStatistics = new FlightStatistics()
        {
            ClassDistribution = GetClassDistributionAsync(flights),
            SeatDistribution = GetSeatDistributionAsync(flights),
            ReasonDistribution = GetReasonDistributionAsync(flights),
            FlightDistribution = GetFlightDistributionAsync(flights),
            Continents = await GetContinentsAsync(flights),
            TopAirports = GetTopAirportsAsync(flights),
            TopAirlines = GetTopAirlinesAsync(flights),
            TopAircrafts = GetTopAircraftAsync(flights),
            FlightRoutes = GetTopFlightRoutesAsync(flights),
            FlightsPerMonth = GetFlightsPerMonthAsync(flights),
            FlightsPerWeek = GetFlightsPerWeekAsync(flights),
            Distance = GetDistanceAsync(flights),
            TotalFlightTime = GetTotalFlightTimeAsync(flights)
        };
        
        return flightStatistics;
    }

    public Dictionary<ClassType, int> GetClassDistributionAsync(List<Flight> flights)
    {
        var classDistribution = flights
            .GroupBy(x => x.ClassType)
            .Select(group => new
            {
                ClassType = group.Key,
                Count = group.Count()
            })
            .ToDictionary(x => x.ClassType, x => x.Count);
        
        return classDistribution;
    }

    public Dictionary<SeatType, int> GetSeatDistributionAsync(List<Flight> flights)
    {
        var seatDistribution = flights
            .GroupBy(x => x.SeatType)
            .Select(group => new
            {
                SeatType = group.Key,
                Count = group.Count()
            })
            .ToDictionary(x => x.SeatType, x => x.Count);
        
        return seatDistribution;
    }

    public Dictionary<Reason, int> GetReasonDistributionAsync(List<Flight> flights)
    {
        var reasonDistribution = flights
            .GroupBy(x => x.Reason)
            .Select(group => new
            {
                Reason = group.Key,
                Count = group.Count()
            })
            .ToDictionary(x => x.Reason, x => x.Count);
        
        return reasonDistribution;
    }

    public Dictionary<FlightType, int> GetFlightDistributionAsync(List<Flight> flights)
    {
        var flightTypeDistribution = new Dictionary<FlightType, int>
        {
            { FlightType.Domestic, 0 },
            { FlightType.International, 0 }
        };

        foreach (var flight in flights)
        {
            var departureAirport = flight.DepartureAirport;
            var arrivalAirport = flight.ArrivalAirport;

            if (departureAirport.Country == arrivalAirport.Country)
            {
                flightTypeDistribution[FlightType.Domestic]++;
            }
            else
            {
                flightTypeDistribution[FlightType.International]++;
            }
        }

        return flightTypeDistribution;
    }

    public async Task<Dictionary<Continent, int>> GetContinentsAsync(List<Flight> flights)
    {
        var continentCounts = new Dictionary<Continent, int>();

        foreach (var flight in flights)
        {
            // Get continents for departure and arrival airports
            var arrivalContinent = await _countryContinentService.GetContinentByCountryNameAsync(flight.ArrivalAirport.Country);

            if (Enum.TryParse(arrivalContinent, out Continent arrivalEnum))
            {
                continentCounts.TryAdd(arrivalEnum, 0);
                continentCounts[arrivalEnum]++;
            }
            else
            {
                continentCounts.TryAdd(Continent.Unknown, 0);
                continentCounts[Continent.Unknown]++;
            }
        }

        return continentCounts;
    }

    public Dictionary<string, int> GetTopAirportsAsync(List<Flight> flights)
    {
        var topAirports = flights
            .SelectMany(flight => new[] { flight.DepartureAirport, flight.ArrivalAirport })
            .GroupBy(airport => airport.IataCode)
            .Select(group => new
            {
                AirportCode = group.Key,
                Count = group.Count()
            })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .ToDictionary(x => x.AirportCode, x => x.Count);

        return topAirports;
    }

    public Dictionary<string, int> GetTopAirlinesAsync(List<Flight> flights)
    {
        var topAirlines = flights
            .GroupBy(x => x.Airline.IataCode)
            .Select(group => new
            {
                AirlineCode = group.Key,
                Count = group.Count()
            })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .ToDictionary(airline => airline.AirlineCode, airline => airline.Count);
        
        return topAirlines;
    }

    public Dictionary<string, int> GetTopAircraftAsync(List<Flight> flights)
    {
        var topAircrafts = flights
            .GroupBy(x => x.Aircraft.IcaoCode)
            .Select(group => new
            {
                AircraftCode = group.Key,
                Count = group.Count()
            })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .ToDictionary(airline => airline.AircraftCode, airline => airline.Count);
        
        return topAircrafts;
    }

    public Dictionary<string, int> GetTopFlightRoutesAsync(List<Flight> flights)
    {
        var flightRoutes = flights
            .GroupBy(x => $"{x.DepartureAirport.IataCode} - {x.ArrivalAirport.IataCode}")
            .Select(group => new
            {
                Route = group.Key,
                Count = group.Count()
            })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .ToDictionary(x => x.Route, x => x.Count);

        return flightRoutes;
    }

    public Dictionary<Month, int> GetFlightsPerMonthAsync(List<Flight> flights, int? year = null)
    {
        year ??= DateTime.Now.Year;
    
        var flightsPerMonth = Enum.GetValues(typeof(Month))
            .Cast<Month>()
            .ToDictionary(m => m, _ => 0);
    
        var groupedFlights = flights
            .Where(x => x.ArrivalTime.Year == year)
            .GroupBy(x => x.ArrivalTime.Month)
            .Select(group => new
            {
                Month = (Month)group.Key,
                Count = group.Count()
            });
        
        foreach (var group in groupedFlights)
        {
            flightsPerMonth[group.Month] = group.Count;
        }

        return flightsPerMonth;
    }

    public Dictionary<DayOfWeek, int> GetFlightsPerWeekAsync(List<Flight> flights, int? weekNumber = null)
    {
        weekNumber ??= StatisticsHelper.GetWeekNumber(DateTime.Now);

        var startOfWeek = StatisticsHelper.GetStartOfWeek(DateTime.Now, weekNumber.Value);

        var filteredFlights = flights.Where(f =>
                f.ArrivalTime >= startOfWeek &&
                f.ArrivalTime < startOfWeek.AddDays(7))
            .ToList();

        var flightsPerWeek = filteredFlights
            .GroupBy(f => f.ArrivalTime.DayOfWeek)
            .Select(group => new
            {
                Day = group.Key,
                Count = group.Count()
            })
            .ToDictionary(x => x.Day, x => x.Count);

        var allDaysOfWeek = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>().ToList();
        foreach (var day in allDaysOfWeek)
        {
            if (!flightsPerWeek.ContainsKey(day))
            {
                flightsPerWeek[day] = 0;
            }
        }

        return flightsPerWeek;
    }

    public Distance GetDistanceAsync(List<Flight> flights)
    {
        var distance = new Distance();
        
        foreach (var flight in flights)
        {
            distance.TotalDistance += StatisticsHelper.CalculateDistance(flight.DepartureAirport.Latitude, flight.DepartureAirport.Longitude, flight.ArrivalAirport.Latitude, flight.ArrivalAirport.Longitude);
        }
        
        return distance;
    }

    public TimeSpan GetTotalFlightTimeAsync(List<Flight> flights)
    {
        TimeSpan totalFlightTime = TimeSpan.Zero;

        foreach (var flight in flights)
        {
            totalFlightTime += flight.ArrivalTime - flight.DepartureTime;
        }

        return totalFlightTime;
    }
}