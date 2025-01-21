using FlightSaverApi.Enums;
using FlightSaverApi.Enums.FlightEnums;
using FlightSaverApi.Models;
using FlightSaverApi.Models.Review;

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
            var users = new[]
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
            var airports = new[]
            {
                new Airport { IcaoCode = "KJFK", IataCode = "JFK", Name = "John F. Kennedy International Airport", City = "New York", Country = "USA", Latitude = 40.6413, Longitude = -73.7781 },
                new Airport { IcaoCode = "EGLL", IataCode = "LHR", Name = "London Heathrow Airport", City = "London", Country = "GB", Latitude = 51.4700, Longitude = -0.4543 },
                new Airport { IcaoCode = "OMDB", IataCode = "DXB", Name = "Dubai International Airport", City = "Dubai", Country = "AE", Latitude = 25.276987, Longitude = 55.296249 },
                new Airport { IcaoCode = "KSFO", IataCode = "SFO", Name = "San Francisco International Airport", City = "San Francisco", Country = "USA", Latitude = 37.7749, Longitude = -122.4194 }
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
            var airlines = new[]
            {
                new Airline { Name = "American Airlines", IataCode = "AA", IcaoCode = "AAL", Country = "USA" },
                new Airline { Name = "British Airways", IataCode = "BA", IcaoCode = "BAW", Country = "GB" },
                new Airline { Name = "Emirates", IataCode = "EK", IcaoCode = "UAE", Country = "AE" },
                new Airline { Name = "Delta Airlines", IataCode = "DL", IcaoCode = "DAL", Country = "USA" },
                new Airline { Name = "United Airlines", IataCode = "UA", IcaoCode = "UAL", Country = "USA" }
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
            var aircrafts = new[]
            {
                new Aircraft { Name = "Boeing 777", IataCode = "777", IcaoCode = "B77W", RegNumber = "N12345", AirlineId = airlines.FirstOrDefault(a => a.IataCode == "AA")?.Id ?? 1 },
                new Aircraft { Name = "Airbus A380", IataCode = "380", IcaoCode = "A38X", RegNumber = "G54321", AirlineId = airlines.FirstOrDefault(a => a.IataCode == "BA")?.Id ?? 2 },
                new Aircraft { Name = "Boeing 737", IataCode = "737", IcaoCode = "B737", RegNumber = "N67890", AirlineId = airlines.FirstOrDefault(a => a.IataCode == "UA")?.Id ?? 3 },
                new Aircraft { Name = "Airbus A320", IataCode = "320", IcaoCode = "A32X", RegNumber = "N11223", AirlineId = airlines.FirstOrDefault(a => a.IataCode == "DL")?.Id ?? 4 }
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
            var flights = new[]
            {
                new Flight { 
                    FlightNumber = "AA100", 
                    DepartureAirportId = 1, 
                    ArrivalAirportId = 2, 
                    AirlineId = 1, 
                    AircraftId = 1, 
                    DepartureTime = DateTime.UtcNow.AddHours(2),
                    ArrivalTime = DateTime.UtcNow.AddHours(6),
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
                    DepartureTime = DateTime.UtcNow.AddHours(3),
                    ArrivalTime = DateTime.UtcNow.AddHours(7),
                    ClassType = ClassType.Economy, 
                    SeatType = SeatType.Aisle, 
                    SeatNumber = "25C", 
                    Reason = Reason.Business, 
                    UserId = 3 
                },
                new Flight { 
                    FlightNumber = "EK300", 
                    DepartureAirportId = 3, 
                    ArrivalAirportId = 4, 
                    AirlineId = 3, 
                    AircraftId = 3, 
                    DepartureTime = DateTime.UtcNow.AddHours(4),
                    ArrivalTime = DateTime.UtcNow.AddHours(8),
                    ClassType = ClassType.First, 
                    SeatType = SeatType.Window, 
                    SeatNumber = "1D", 
                    Reason = Reason.Leisure, 
                    UserId = 3 
                },
                new Flight { 
                    FlightNumber = "UA400", 
                    DepartureAirportId = 4, 
                    ArrivalAirportId = 3, 
                    AirlineId = 4, 
                    AircraftId = 4, 
                    DepartureTime = DateTime.UtcNow.AddHours(5),
                    ArrivalTime = DateTime.UtcNow.AddHours(9),
                    ClassType = ClassType.Economy, 
                    SeatType = SeatType.Aisle, 
                    SeatNumber = "15B", 
                    Reason = Reason.Business, 
                    UserId = 3
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
                // Reviews for Flight AA100
                new AircraftReview { Rating = 5, Comment = "Great aircraft!", UserId = 2, FlightId = 1, AircraftId = 1 },
                new AirlineReview { Rating = 4, Comment = "Good service, but the food could be better.", UserId = 2, FlightId = 1, AirlineId = 1 },
                new AirportReview { Rating = 4, Comment = "Nice airport, but security was a bit slow.", UserId = 2, FlightId = 1, AirportId = 1, AirportReviewType = AirportReviewType.Departure },
                new AirportReview { Rating = 5, Comment = "Smooth experience at arrival.", UserId = 2, FlightId = 2, AirportId = 2, AirportReviewType = AirportReviewType.Arrival },
                
                // Additional Reviews for Flight EK300
                new AircraftReview { Rating = 5, Comment = "Comfortable long-haul aircraft.", UserId = 2, FlightId = 3, AircraftId = 3 },
                new AirlineReview { Rating = 5, Comment = "Excellent service, would fly with Emirates again.", UserId = 2, FlightId = 3, AirlineId = 3 },
                new AirportReview { Rating = 4, Comment = "Great amenities, but it was crowded.", UserId = 2, FlightId = 3, AirportId = 3, AirportReviewType = AirportReviewType.Departure },
                new AirportReview { Rating = 5, Comment = "Efficient and quick arrival process.", UserId = 2, FlightId = 4, AirportId = 4, AirportReviewType = AirportReviewType.Arrival },
        
                // Additional Reviews for Flight UA400
                new AircraftReview { Rating = 4, Comment = "The seating was a bit cramped, but the flight was smooth.", UserId = 2, FlightId = 4, AircraftId = 4 },
                new AirlineReview { Rating = 4, Comment = "Good flight overall, but lacked entertainment options.", UserId = 2, FlightId = 4, AirlineId = 4 },
                new AirportReview { Rating = 5, Comment = "Fantastic airport experience, fast check-in.", UserId = 2, FlightId = 4, AirportId = 4, AirportReviewType = AirportReviewType.Departure },
                new AirportReview { Rating = 4, Comment = "Customs was a bit slow, but overall good experience.", UserId = 2, FlightId = 4, AirportId = 3, AirportReviewType = AirportReviewType.Arrival }
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
