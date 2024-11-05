using System;
using FlightSaverApi.Models.AircraftModel;
using FlightSaverApi.Models.AirlineModel;
using FlightSaverApi.Models.AirportModel;
using FlightSaverApi.Models.FlightModel;
using FlightSaverApi.Models.ReviewModel;
using FlightSaverApi.Models.UserModel;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Data
{
    public class FlightSaverContext : DbContext
    {
        public FlightSaverContext(DbContextOptions<FlightSaverContext> options) : base(options)
        {
        }

        public DbSet<Aircraft> Aircrafts { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Airline> Airlines { get; set; } = null!;
        public DbSet<Airport> Airports { get; set; } = null!;
        public DbSet<Flight> Flights { get; set; } = null!;
        public DbSet<AircraftReview> AircraftReviews { get; set; } = null!;
        public DbSet<AirlineReview> AirlineReviews { get; set; } = null!;
        public DbSet<AirportReview> AirportReviews { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aircraft>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.IataCode).IsRequired().HasMaxLength(3);
                entity.Property(e => e.IcaoCode).IsRequired().HasMaxLength(4);
                entity.Property(e => e.RegNumber).IsRequired().HasMaxLength(10);
                entity.Property(e => e.AircraftUrl).HasMaxLength(255);
                entity.HasOne(a => a.Airline)
                    .WithMany()
                    .HasForeignKey(a => a.AirlineId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Username).IsUnique();
                entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Role).IsRequired();
            });

            modelBuilder.Entity<Airline>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(150);
                entity.Property(e => e.IataCode).IsRequired().HasMaxLength(3);
                entity.Property(e => e.IcaoCode).IsRequired().HasMaxLength(4);
                entity.Property(e => e.Country).IsRequired().HasMaxLength(100);
                entity.Property(e => e.LogoUrl).HasMaxLength(255);
            });

            modelBuilder.Entity<Airport>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.IataCode).IsRequired().HasMaxLength(3);
                entity.Property(e => e.IcaoCode).IsRequired().HasMaxLength(4);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(150);
                entity.Property(e => e.City).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Country).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Latitude).IsRequired();
                entity.Property(e => e.Longitude).IsRequired();
            });

            modelBuilder.Entity<Flight>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FlightNumber).IsRequired().HasMaxLength(20);
            });

            modelBuilder.Entity<AircraftReview>(entity =>
            {

            });

            modelBuilder.Entity<AirlineReview>(entity =>
            {

            });

            modelBuilder.Entity<AirportReview>(entity =>
            {

            });
        }
    }
}

