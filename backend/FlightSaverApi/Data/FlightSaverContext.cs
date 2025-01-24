using FlightSaverApi.Models;
using FlightSaverApi.Models.Review;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Data
{
    public class FlightSaverContext : DbContext
    {
        public FlightSaverContext(DbContextOptions<FlightSaverContext> options) : base(options)
        {
        }

        public DbSet<Airport> Airports { get; set; }
        public DbSet<Airline> Airlines { get; set; }
        public DbSet<Aircraft> Aircrafts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<AircraftReview> AircraftReviews { get; set; }
        public DbSet<AirlineReview> AirlineReviews { get; set; }
        public DbSet<AirportReview> AirportReviews { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<SocialPost> SocialPosts { get; set; }        
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PostLike> PostLikes { get; set; }
        public DbSet<CommentLike> CommentLikes { get; set; }
        public DbSet<ImageRecord> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FlightSaverContext).Assembly);
        }
    }
}

