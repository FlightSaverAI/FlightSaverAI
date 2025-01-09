using FlightSaverApi.Models;
using FlightSaverApi.Models.ReviewModel;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Airport>(entity =>
            {
                entity.Property(a => a.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasIndex(a => a.IataCode).IsUnique();
                entity.Property(a => a.IataCode)
                    .IsRequired()
                    .HasMaxLength(3);

                entity.HasIndex(a => a.IcaoCode).IsUnique();
                entity.Property(a => a.IcaoCode)
                    .IsRequired()
                    .HasMaxLength(4);

                entity.Property(a => a.City)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(a => a.Country)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(a => a.Latitude)
                    .IsRequired()
                    .HasColumnType("decimal(9, 6)");

                entity.Property(a => a.Longitude)
                    .IsRequired()
                    .HasColumnType("decimal(9, 6)");
                
                entity
                    .HasMany(a => a.DepartingFlights)
                    .WithOne(f => f.DepartureAirport)
                    .HasForeignKey(f => f.DepartureAirportId);
                
                entity
                    .HasMany(a => a.ArrivingFlights)
                    .WithOne(f => f.ArrivalAirport)
                    .HasForeignKey(r => r.ArrivalAirportId);
            });

            modelBuilder.Entity<Airline>(entity =>
            {
                entity.Property(a => a.Name)
                    .IsRequired()
                    .HasMaxLength(100);
                
                entity.HasIndex(a => a.IataCode).IsUnique();
                entity.Property(a => a.IataCode)
                    .IsRequired()
                    .HasMaxLength(3);

                entity.HasIndex(a => a.IcaoCode).IsUnique();
                entity.Property(a => a.IcaoCode)
                    .IsRequired()
                    .HasMaxLength(4);

                entity.Property(a => a.Country)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(a => a.LogoUrl)
                    .HasMaxLength(255);

                entity
                    .HasMany(a => a.Aircrafts)
                    .WithOne()
                    .HasForeignKey(a => a.AirlineId)
                    .IsRequired();
                
                entity
                    .HasMany(a => a.AirlineReviews)
                    .WithOne()
                    .HasForeignKey(r => r.AirlineId);
                
                entity
                    .HasMany(a => a.Flights)
                    .WithOne()
                    .HasForeignKey(r => r.AirlineId);
            });

            modelBuilder.Entity<Aircraft>(entity =>
            {
                entity.Property(a => a.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(a => a.IataCode)
                    .IsRequired()
                    .HasMaxLength(3);

                entity.Property(a => a.IcaoCode)
                    .IsRequired()
                    .HasMaxLength(4);

                entity.HasIndex(a => a.RegNumber).IsUnique();
                entity.Property(a => a.RegNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(a => a.AircraftUrl)
                    .HasMaxLength(255);

                entity
                    .HasOne(a => a.Airline)
                    .WithMany(a => a.Aircrafts)
                    .HasForeignKey(a => a.AirlineId)
                    .IsRequired();

                entity
                    .HasMany(a => a.AircraftReviews)
                    .WithOne()
                    .HasForeignKey(r => r.AircraftId);
                
                entity
                    .HasMany(a => a.Flights)
                    .WithOne()
                    .HasForeignKey(r => r.AircraftId);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(a => a.Username).IsUnique();
                entity.Property(u => u.Username)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasIndex(a => a.Email).IsUnique();
                entity.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(u => u.Role)
                    .IsRequired();

                entity.Property(u => u.PasswordHash)
                    .IsRequired();

                entity.Property(u => u.PasswordSalt)
                    .IsRequired();

                entity
                    .HasMany(u => u.AircraftReviews)
                    .WithOne()
                    .HasForeignKey(r => r.UserId);
                
                entity
                    .HasMany(u => u.AirlineReviews)
                    .WithOne()
                    .HasForeignKey(r => r.UserId);
                
                entity
                    .HasMany(u => u.AirportReviews)
                    .WithOne()
                    .HasForeignKey(r => r.UserId);
                
                entity
                    .HasMany(a => a.Flights)
                    .WithOne()
                    .HasForeignKey(r => r.UserId);
            });
            
            modelBuilder.Entity<AircraftReview>(entity =>
            {
                entity.Property(u => u.Rating)
                    .IsRequired();

                entity.Property(u => u.Comment)
                    .HasMaxLength(200);

                entity
                    .HasOne(a => a.Aircraft)
                    .WithMany(a => a.AircraftReviews)
                    .HasForeignKey(a => a.AircraftId)
                    .IsRequired();

                entity
                    .HasOne(r => r.User)
                    .WithMany(u => u.AircraftReviews)
                    .HasForeignKey(r => r.UserId)
                    .IsRequired();
                
                entity
                    .HasOne(r => r.Flight)
                    .WithOne(r => r.AircraftReview)
                    .HasForeignKey<AircraftReview>(r => r.FlightId)
                    .IsRequired();
            });
            
            modelBuilder.Entity<AirlineReview>(entity =>
            {
                entity.Property(u => u.Rating)
                    .IsRequired();

                entity.Property(u => u.Comment)
                    .HasMaxLength(200);

                entity
                    .HasOne(a => a.Airline)
                    .WithMany(a => a.AirlineReviews)
                    .HasForeignKey(a => a.AirlineId)
                    .IsRequired();

                entity
                    .HasOne(r => r.User)
                    .WithMany(u => u.AirlineReviews)
                    .HasForeignKey(r => r.UserId)
                    .IsRequired();
                
                entity
                    .HasOne(r => r.Flight)
                    .WithOne(r => r.AirlineReview)
                    .HasForeignKey<AirlineReview>(r => r.FlightId)
                    .IsRequired();
            });
            
            modelBuilder.Entity<AirportReview>(entity =>
            {
                entity.Property(u => u.Rating)
                    .IsRequired();

                entity.Property(u => u.Comment)
                    .HasMaxLength(200);

                entity
                    .HasOne(a => a.Airport)
                    .WithMany(a => a.AirportReviews)
                    .HasForeignKey(a => a.AirportId)
                    .IsRequired();

                entity
                    .HasOne(r => r.User)
                    .WithMany(u => u.AirportReviews)
                    .HasForeignKey(r => r.UserId)
                    .IsRequired();

                entity
                    .HasOne(r => r.Flight)
                    .WithMany(r => r.AirportReviews)
                    .HasForeignKey(r => r.FlightId)
                    .IsRequired();
            });

            modelBuilder.Entity<Flight>(entity =>
            {
                entity.Property(f => f.FlightNumber)
                    .IsRequired()
                    .HasMaxLength(50);
                
                entity.Property(f => f.DepartureTime)
                    .IsRequired();
                
                entity.Property(f => f.ArrivalTime)
                    .IsRequired();
                
                entity.Property(f => f.ClassType)
                    .IsRequired();
                
                entity.Property(f => f.SeatType)
                    .IsRequired();
                
                entity.Property(f => f.SeatNumber)
                    .IsRequired();
                
                entity.Property(f => f.Reason)
                    .IsRequired();
                
                entity
                    .HasOne(f => f.DepartureAirport)
                    .WithMany(a => a.DepartingFlights)
                    .HasForeignKey(f => f.DepartureAirportId)
                    .IsRequired();
                
                entity
                    .HasOne(f => f.ArrivalAirport)
                    .WithMany(a => a.ArrivingFlights)
                    .HasForeignKey(f => f.ArrivalAirportId)
                    .IsRequired();
                
                entity
                    .HasOne(f => f.Airline)
                    .WithMany(a => a.Flights)
                    .HasForeignKey(f => f.AirlineId)
                    .IsRequired();
                
                entity
                    .HasOne(f => f.Aircraft)
                    .WithMany(a => a.Flights)
                    .HasForeignKey(f => f.AircraftId)
                    .IsRequired();

                entity.HasMany(f => f.AirportReviews)
                    .WithOne(r => r.Flight)
                    .HasForeignKey(r => r.FlightId)
                    .IsRequired(false);
                
                entity
                    .HasOne(f => f.AircraftReview)
                    .WithOne(f => f.Flight)
                    .HasForeignKey<AircraftReview>(f => f.FlightId)
                    .IsRequired(false);
                
                entity
                    .HasOne(f => f.AirlineReview)
                    .WithOne(f => f.Flight)
                    .HasForeignKey<AirlineReview>(f => f.FlightId)
                    .IsRequired(false);

                entity
                    .HasOne(f => f.User)
                    .WithMany(u => u.Flights)
                    .HasForeignKey(f => f.UserId)
                    .IsRequired();
            });
        }
    }
}

