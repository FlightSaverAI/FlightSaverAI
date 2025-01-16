using FlightSaverApi.Enums;
using FlightSaverApi.Models.Review;

namespace FlightSaverApi.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public UserRole Role { get; set; } = UserRole.User;

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }
        
        public string? ProfilePictureUrl { get; set; }

        public virtual List<AircraftReview>? AircraftReviews { get; set; }
        
        public virtual List<AirlineReview>? AirlineReviews { get; set; }
        
        public virtual List<AirportReview>? AirportReviews { get; set; }
        
        public virtual List<Flight>? Flights { get; set; }
    }
}

