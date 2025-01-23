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
            ClassDistribution = GetClassDistribution(flights),
            SeatDistribution = GetSeatDistribution(flights),
            ReasonDistribution = GetReasonDistribution(flights),
            Continents = await GetContinentsAsync(flights),
            TopAirports = GetTopAirports(flights),
            TopAirlines = GetTopAirlines(flights),
            TopAircrafts = GetTopAircraft(flights),
            FlightRoutes = GetTopFlightRoutes(flights),
            FlightsPerMonth = GetFlightsPerMonth(flights),
            FlightsPerWeek = GetFlightsPerWeek(flights),
        };
        
        return flightStatistics;
    }

    public async Task<CircualChartStatistics> GetCircualChartStatisticsAsync(int userId,
        CancellationToken cancellationToken = default)
    {
        var flights = await _flightsService.GetFlightsByUserIdAsync(userId, cancellationToken);
        
        if (flights.Count == 0) return new CircualChartStatistics();
        
        var flightStatistics = new CircualChartStatistics()
        {
            ClassDistribution = GetCircualDataClassDistribution(flights),
            SeatDistribution = GetCircualDataSeatDistribution(flights),
            ReasonDistribution = GetCircualDataReasonDistribution(flights),
            Continents = await GetCircualDataContinentsAsync(flights),
        };
        
        return flightStatistics;
    }
    
    public async Task<BarChartStatistics> GetBarChartStatisticsAsync(int userId,
        CancellationToken cancellationToken = default)
    {
        var flights = await _flightsService.GetFlightsByUserIdAsync(userId, cancellationToken);
        
        if (flights.Count == 0) return new BarChartStatistics();
        
        var flightStatistics = new BarChartStatistics()
        {
            TopAirports = GetTopAirports(flights),
            TopAirlines = GetTopAirlines(flights),
            TopAircrafts = GetTopAircraft(flights),
            FlightRoutes = GetTopFlightRoutes(flights),
        };
        
        return flightStatistics;
    }
    
    public async Task<LineChartStatistics> GetLineChartStatisticsAsync(int userId,
        CancellationToken cancellationToken = default)
    {
        var flights = await _flightsService.GetFlightsByUserIdAsync(userId, cancellationToken);
        
        if (flights.Count == 0) return new LineChartStatistics();
        
        var flightStatistics = new LineChartStatistics()
        {
            FlightsPerMonth = GetFlightsPerMonth(flights),
            FlightsPerWeek = GetFlightsPerWeek(flights),
        };
        
        return flightStatistics;
    }

    public async Task<BasicFlightStatistics> GetBasicFlightStatisticsAsync(int userId,
        CancellationToken cancellationToken = default)
    {
        var flights = await _flightsService.GetFlightsByUserIdAsync(userId, cancellationToken);
        
        if (flights.Count == 0) return new BasicFlightStatistics();

        var basicFlightStatistics = new BasicFlightStatistics()
        {
            Distance = GetDistance(flights),
            FlightCount = GetFlightCount(flights),
            TotalFlightTime = GetTotalFlightTime(flights)
        };
        
        return basicFlightStatistics;
    }

    public Dictionary<ClassType, int> GetClassDistribution(List<Flight> flights)
    {
        var classDistribution = Enum.GetValues(typeof(ClassType))
            .Cast<ClassType>()
            .ToDictionary(classType => classType, _ => 0);

        var groupedFlights = flights
            .GroupBy(flight => flight.ClassType)
            .Select(group => new
            {
                ClassType = group.Key,
                Count = group.Count()
            });

        foreach (var group in groupedFlights)
        {
            classDistribution[group.ClassType] = group.Count;
        }
        
        return classDistribution;
    }

    public List<CircualData> GetCircualDataClassDistribution (List<Flight> flights)
    {
        var classDistribution = Enum.GetValues(typeof(ClassType))
            .Cast<ClassType>()
            .ToDictionary(classType => classType, _ => 0);

        var groupedFlights = flights
            .GroupBy(flight => flight.ClassType)
            .Select(group => new
            {
                ClassType = group.Key,
                Count = group.Count()
            });

        foreach (var group in groupedFlights)
        {
            classDistribution[group.ClassType] = group.Count;
        }

        return classDistribution.Select(kv => new CircualData
        {
            Name = kv.Key == ClassType.EconomyPlus ? "Economy+" : kv.Key.ToString(),
            Value = kv.Value
        }).ToList();
    }

    public Dictionary<SeatType, int> GetSeatDistribution(List<Flight> flights)
    {
        var seatDistribution = Enum.GetValues(typeof(SeatType))
            .Cast<SeatType>()
            .ToDictionary(seatType => seatType, _ => 0);

        var groupedFlights = flights
            .GroupBy(flight => flight.SeatType)
            .Select(group => new
            {
                SeatType = group.Key,
                Count = group.Count()
            });

        foreach (var group in groupedFlights)
        {
            seatDistribution[group.SeatType] = group.Count;
        }

        return seatDistribution;
    }
    
    public List<CircualData> GetCircualDataSeatDistribution (List<Flight> flights)
    {
        var classDistribution = Enum.GetValues(typeof(SeatType))
            .Cast<SeatType>()
            .ToDictionary(classType => classType, _ => 0);

        var groupedFlights = flights
            .GroupBy(flight => flight.SeatType)
            .Select(group => new
            {
                SeatType = group.Key,
                Count = group.Count()
            });

        foreach (var group in groupedFlights)
        {
            classDistribution[group.SeatType] = group.Count;
        }

        return classDistribution.Select(kv => new CircualData
        {
            Name = kv.Key.ToString(),
            Value = kv.Value
        }).ToList();
    }

    public Dictionary<Reason, int> GetReasonDistribution(List<Flight> flights)
    {
        var reasonDistribution = Enum.GetValues(typeof(Reason))
            .Cast<Reason>()
            .ToDictionary(reason => reason, _ => 0);

        var groupedFlights = flights
            .GroupBy(flight => flight.Reason)
            .Select(group => new
            {
                Reason = group.Key,
                Count = group.Count()
            });

        foreach (var group in groupedFlights)
        {
            reasonDistribution[group.Reason] = group.Count;
        }

        return reasonDistribution;
    }
    
    public List<CircualData> GetCircualDataReasonDistribution (List<Flight> flights)
    {
        var classDistribution = Enum.GetValues(typeof(Reason))
            .Cast<Reason>()
            .ToDictionary(classType => classType, _ => 0);

        var groupedFlights = flights
            .GroupBy(flight => flight.Reason)
            .Select(group => new
            {
                Reason = group.Key,
                Count = group.Count()
            });

        foreach (var group in groupedFlights)
        {
            classDistribution[group.Reason] = group.Count;
        }

        return classDistribution.Select(kv => new CircualData
        {
            Name = kv.Key.ToString(),
            Value = kv.Value
        }).ToList();
    }

    public FlightCount GetFlightCount(List<Flight> flights)
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
        
        var flightCount = new FlightCount()
        {
            Count = flights.Count(),
            FlightDistribution = flightTypeDistribution
        };

        return flightCount;
    }

    public async Task<Dictionary<Continent, int>> GetContinentsAsync(List<Flight> flights)
    {
        var continentCounts = Enum.GetValues(typeof(Continent))
            .Cast<Continent>()
            .Where(continent => continent != Continent.Unknown)
            .ToDictionary(continent => continent, _ => 0);

        foreach (var flight in flights)
        {
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
    
    public async Task<List<CircualData>> GetCircualDataContinentsAsync (List<Flight> flights)
    {
        var continentDistribution = Enum.GetValues(typeof(Continent))
            .Cast<Continent>()
            .Where(continent => continent != Continent.Unknown)  // Exclude Unknown
            .ToDictionary(continent => continent, _ => 0);

        // Group flights by continent and count them
        var groupedFlights = flights
            .GroupBy(flight => flight.ArrivalAirport.Country)
            .Select(group => new
            {
                Country = group.Key,
                Count = group.Count()
            });

        // Loop through each flight group and assign the count to the correct continent
        foreach (var group in groupedFlights)
        {
            var arrivalContinent = await _countryContinentService.GetContinentByCountryNameAsync(group.Country);

            if (Enum.TryParse(arrivalContinent, out Continent arrivalEnum))
            {
                continentDistribution[arrivalEnum] += group.Count;
            }
        }

        // Convert the continent distribution to a list of ContinentDistributionItem
        return continentDistribution.Select(kv => new CircualData()
        {
            Name = kv.Key.ToString(),
            Value = kv.Value
        }).ToList();
    }

    public Dictionary<string, int> GetTopAirports(List<Flight> flights)
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

    public Dictionary<string, int> GetTopAirlines(List<Flight> flights)
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

    public Dictionary<string, int> GetTopAircraft(List<Flight> flights)
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

    public Dictionary<string, int> GetTopFlightRoutes(List<Flight> flights)
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

    public Dictionary<Month, int> GetFlightsPerMonth(List<Flight> flights, int? year = null)
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

    public Dictionary<DayOfWeek, int> GetFlightsPerWeek(List<Flight> flights, int? weekNumber = null)
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

    public Distance GetDistance(List<Flight> flights)
    {
        var distance = new Distance();
        
        foreach (var flight in flights)
        {
            distance.TotalDistance += StatisticsHelper.CalculateDistance(flight.DepartureAirport.Latitude, flight.DepartureAirport.Longitude, flight.ArrivalAirport.Latitude, flight.ArrivalAirport.Longitude);
        }
        
        return distance;
    }

    public FlightTime GetTotalFlightTime(List<Flight> flights)
    {
        TimeSpan totalFlightTime = TimeSpan.Zero;

        foreach (var flight in flights)
        {
            totalFlightTime += flight.ArrivalTime - flight.DepartureTime;
        }
        
        var flightTime = new FlightTime()
        {
            Time = totalFlightTime,
        };

        return flightTime;
    }
}