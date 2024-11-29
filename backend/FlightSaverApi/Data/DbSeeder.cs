using FlightSaverApi.Enums;
using FlightSaverApi.Enums.FlightEnums;
using FlightSaverApi.Models.AircraftModel;
using FlightSaverApi.Models.AirlineModel;
using FlightSaverApi.Models.AirportModel;
using FlightSaverApi.Models.FlightModel;
using FlightSaverApi.Models.ReviewModel;
using FlightSaverApi.Models.UserModel;

namespace FlightSaverApi.Data;

public class DbSeeder
{
    public static void Seed(FlightSaverContext context)
    {
        // Ensure the database is created
        context.Database.EnsureCreated();

        // Add Users if not already present
        if (!context.Users.Any())
        {
            var users = new User[]
            {
                new User { Username = "admin", Email = "admin@example.com", Role = UserRole.Admin, PasswordHash = new byte[] { 1, 2, 3 }, PasswordSalt = new byte[] { 4, 5, 6 }},
                new User { Username = "john_doe", Email = "john@example.com", Role = UserRole.User, PasswordHash = new byte[] { 7, 8, 9 }, PasswordSalt = new byte[] { 10, 11, 12 }}
            };
            foreach (var u in users)
            {
                context.Users.Add(u);
            }
            context.SaveChanges();
        }

        // Add Airports if not already present
        if (!context.Airports.Any())
        {
            var airports = new Airport[]
            {
                new Airport { IcaoCode = "KJFK", IataCode = "JFK", Name = "John F. Kennedy International Airport", City = "New York", Country = "USA", Latitude = 40.6413, Longitude = -73.7781 },
                new Airport { IcaoCode = "EGLL", IataCode = "LHR", Name = "London Heathrow Airport", City = "London", Country = "UK", Latitude = 51.4700, Longitude = -0.4543 }
            };
            foreach (var a in airports)
            {
                context.Airports.Add(a);
            }
            context.SaveChanges();
        }

        // Add Airlines if not already present
        if (!context.Airlines.Any())
        {
            var airlines = new Airline[]
            {
                new Airline { Name = "American Airlines", IataCode = "AA", IcaoCode = "AAL", Country = "USA" },
                new Airline { Name = "British Airways", IataCode = "BA", IcaoCode = "BAW", Country = "UK" }
            };

            foreach (var a in airlines)
            {
                context.Airlines.Add(a);
            }
            context.SaveChanges(); // Save to get the actual AirlineIds
        }

        // Add Aircrafts if not already present
        if (!context.Aircrafts.Any())
        {
            var airlines = context.Airlines.ToList(); // Ensure Airlines are loaded and have valid Ids
            var aircrafts = new Aircraft[]
            {
                new Aircraft { Name = "Boeing 777", IataCode = "777", IcaoCode = "B77W", RegNumber = "N12345", AirlineId = airlines.FirstOrDefault(a => a.IataCode == "AA")?.Id ?? 1 },
                new Aircraft { Name = "Airbus A380", IataCode = "380", IcaoCode = "A38X", RegNumber = "G54321", AirlineId = airlines.FirstOrDefault(a => a.IataCode == "BA")?.Id ?? 2 }
            };

            foreach (var a in aircrafts)
            {
                context.Aircrafts.Add(a);
            }
            context.SaveChanges();
        }

        // Add Flights if not already present
        if (!context.Flights.Any())
        {
            var flights = new Flight[]
            {
                new Flight { 
                    FlightNumber = "AA100", 
                    DepartureAirportId = 1, 
                    ArrivalAirportId = 2, 
                    AirlineId = 1, 
                    AircraftId = 1, 
                    DepartureTime = DateTime.UtcNow.AddHours(2),  // Use UTC time
                    ArrivalTime = DateTime.UtcNow.AddHours(6),    // Use UTC time
                    ClassType = ClassType.Business, 
                    SeatType = SeatType.Window, 
                    SeatNumber = "1A", 
                    Reason = Reason.Leisure, 
                    UserId = 2 
                },
                new Flight { 
                    FlightNumber = "BA200", 
                    DepartureAirportId = 2, 
                    ArrivalAirportId = 1, 
                    AirlineId = 2, 
                    AircraftId = 2, 
                    DepartureTime = DateTime.UtcNow.AddHours(3),  // Use UTC time
                    ArrivalTime = DateTime.UtcNow.AddHours(7),    // Use UTC time
                    ClassType = ClassType.Economy, 
                    SeatType = SeatType.Aisle, 
                    SeatNumber = "25C", 
                    Reason = Reason.Business, 
                    UserId = 2 
                }
            };

            foreach (var f in flights)
            {
                context.Flights.Add(f);
            }
            context.SaveChanges();
        }

        // Add Reviews if not already present
        if (!context.AircraftReviews.Any() || !context.AirlineReviews.Any() || !context.AirportReviews.Any())
        {
            var reviews = new Review[]
            {
                new AircraftReview { Rating = 5, Comment = "Great aircraft!", UserId = 2, FlightId = 1, AircraftId = 1 },
                new AirlineReview { Rating = 4, Comment = "Good service, but the food could be better.", UserId = 2, FlightId = 1, AirlineId = 1 },
                new AirportReview { Rating = 4, Comment = "Nice airport, but security was a bit slow.", UserId = 2, FlightId = 1, AirportId = 1, AirportReviewType = AirportReviewType.Departure },
                new AirportReview { Rating = 5, Comment = "Smooth experience at arrival.", UserId = 2, FlightId = 2, AirportId = 2, AirportReviewType = AirportReviewType.Arrival }
            };

            foreach (var r in reviews)
            {
                if (r is AircraftReview aircraftReview)
                    context.AircraftReviews.Add(aircraftReview);
                if (r is AirlineReview airlineReview)
                    context.AirlineReviews.Add(airlineReview);
                if (r is AirportReview airportReview)
                    context.AirportReviews.Add(airportReview);
            }
            context.SaveChanges();
        }
    }
}
