using FlightSaverApi.Data;
using FlightSaverApi.Enums;
using FlightSaverApi.Enums.FlightEnums;
using FlightSaverApi.Helpers;
using FlightSaverApi.Interfaces.Services;
using FlightSaverApi.Models.AircraftModel;
using FlightSaverApi.Models.AirlineModel;
using FlightSaverApi.Models.AirportModel;
using FlightSaverApi.Models.FlightModel;
using FlightSaverApi.Models.StatisticsModel;
using Microsoft.EntityFrameworkCore;

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
            Continents = new Dictionary<Continent, int>(),//await GetContinentsAsync(flights),
            TopAirports = new Dictionary<Airport, int>(), //GetTopAirportsAsync(flights),
            TopAirlines = new Dictionary<Airline, int>(), //GetTopAirlinesAsync(flights),
            TopAircrafts = new Dictionary<Aircraft, int>(), //GetTopAircraftAsync(flights),
            FlightRoutes = new Dictionary<FlightRoute, int>(), //GetTopFlightRoutesAsync(flights),
            FlightsPerMonth = new Dictionary<Month, int>(), //GetFlightsPerMonthAsync(flights),
            FlightsPerWeek = new Dictionary<DayOfWeek, int>(), //GetFlightsPerWeekAsync(flights),
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
        var countryToContinent = await _countryContinentService.FetchCountryToContinentMappingAsync();
        var continentCounts = new Dictionary<Continent, int>();

        foreach (var flight in flights)
        {
            if (countryToContinent.TryGetValue(flight.DepartureAirport.Country, out var departureContinent))
            {
                continentCounts.TryAdd(departureContinent, 0);

                continentCounts[departureContinent]++;
            }
            else
            {
                continentCounts.TryAdd(Continent.Unknown, 0);

                continentCounts[Continent.Unknown]++;
            }

            if (countryToContinent.TryGetValue(flight.ArrivalAirport.Country, out var arrivalContinent))
            {
                continentCounts.TryAdd(arrivalContinent, 0);

                continentCounts[arrivalContinent]++;
            }
            else
            {
                continentCounts.TryAdd(Continent.Unknown, 0);

                continentCounts[Continent.Unknown]++;
            }
        }

        return continentCounts;
    }

    public Dictionary<Airport, int> GetTopAirportsAsync(List<Flight> flights)
    {
        var topAirports =  flights
            .SelectMany(flight => new [] { flight.DepartureAirport, flight.ArrivalAirport})
            .GroupBy(airport => airport)
            .Select(group => new
            {
                Airport = group.Key,
                Count = group.Count()
            })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .ToDictionary(airport => airport.Airport, airport => airport.Count);
        
        return topAirports;
    }

    public Dictionary<Airline, int> GetTopAirlinesAsync(List<Flight> flights)
    {
        var topAirlines = flights
            .GroupBy(x => x.Airline)
            .Select(group => new
            {
                Airline = group.Key,
                Count = group.Count()
            })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .ToDictionary(airline => airline.Airline, airline => airline.Count);
        
        return topAirlines;
    }

    public Dictionary<Aircraft, int> GetTopAircraftAsync(List<Flight> flights)
    {
        var topAircrafts = flights
            .GroupBy(x => x.Aircraft)
            .Select(group => new
            {
                Aircraft = group.Key,
                Count = group.Count()
            })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .ToDictionary(airline => airline.Aircraft, airline => airline.Count);
        
        return topAircrafts;
    }

    public Dictionary<FlightRoute, int> GetTopFlightRoutesAsync(List<Flight> flights)
    {
        var flightRoutes = flights
            .GroupBy(x => new { x.DepartureAirport, x.ArrivalAirport })
            .Select(group => new
            {
                FlightRoute = new FlightRoute(group.Key.DepartureAirport, group.Key.ArrivalAirport),
                Count = group.Count()
            })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .ToDictionary(x => x.FlightRoute, x => x.Count);
        
        return flightRoutes;
    }

    public Dictionary<Month, int> GetFlightsPerMonthAsync(List<Flight> flights, int? year = null)
    {
        year ??= DateTime.Now.Year;
    
        var flightsPerMonth = Enum.GetValues(typeof(Month))
            .Cast<Month>()
            .ToDictionary(m => m, m => 0);
    
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