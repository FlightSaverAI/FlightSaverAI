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

        public DbSet<Airport> Airports { get; set; }
        public DbSet<Airline> Airlines { get; set; }
        public DbSet<Aircraft> Aircrafts { get; set; }
        public DbSet<User> Users { get; set; }
        //public DbSet<AircraftReview> AircraftReviews { get; set; }
        //public DbSet<Flight> Flights { get; set; }
        //public DbSet<AirlineReview> AirlineReviews { get; set; }
        //public DbSet<AirportReview> AirportReviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Airport>(entity =>
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
            });

            modelBuilder.Entity<Airline>(entity =>
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
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u => u.Username)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(u => u.Role)
                    .IsRequired();

                entity.Property(u => u.PasswordHash)
                    .IsRequired();

                entity.Property(u => u.PasswordSalt)
                    .IsRequired();
            });

            //modelBuilder.Entity<AircraftReview>(entity =>
            //{
            //    entity.Property(r => r.Rating)
            //        .IsRequired();

            //    entity.Property(r => r.Comment)
            //        .HasMaxLength(200);

            //    entity
            //        .HasOne(r => r.Reviewer)
            //        .WithMany(u => u.AircraftReviews)
            //        .HasForeignKey(r => r.ReviewerId)
            //        .IsRequired();

            //    entity
            //        .HasOne(r => r.Aircraft)
            //        .WithMany(a => a.AircraftReviews)
            //        .HasForeignKey(r => r.AircraftId)
            //        .IsRequired();
            //});

            //modelBuilder.Entity<Flight>(entity =>
            //{
            //    entity.Property(f => f.FlightNumber)
            //        .IsRequired()
            //        .HasMaxLength(50);

            //    entity.Property(f => f.DepartureTime)
            //        .IsRequired();

            //    entity.Property(f => f.ArrivalTime)
            //        .IsRequired();

            //    entity.Property(f => f.SeatType)
            //        .IsRequired();

            //    entity.Property(f => f.SeatNumber)
            //        .IsRequired()
            //        .HasMaxLength(4);

            //    entity.Property(f => f.ClassType)
            //        .IsRequired();

            //    entity.Property(f => f.Reason)
            //        .HasMaxLength(200);

            //    entity
            //        .HasOne<User>()
            //        .WithMany()
            //        .HasForeignKey(f => f.UserId)
            //        .IsRequired();

            //    entity
            //        .HasOne<Airport>()
            //        .WithMany()
            //        .HasForeignKey(f => f.DepartureAirportId)
            //        .IsRequired()
            //        .HasConstraintName("FK_Flight_DepartureAirport");

            //    entity
            //        .HasOne<Airport>()
            //        .WithMany()
            //        .HasForeignKey(f => f.ArrivalAirportId)
            //        .IsRequired()
            //        .HasConstraintName("FK_Flight_ArrivalAirport");

            //    entity
            //        .HasOne<Airline>()
            //        .WithMany()
            //        .HasForeignKey(f => f.AirlineId)
            //        .IsRequired();

            //    entity
            //        .HasOne<Aircraft>()
            //        .WithMany()
            //        .HasForeignKey(f => f.AircraftId)
            //        .IsRequired()
            //        .HasConstraintName("FK_Flight_Aircraft");

            //    entity
            //        .HasOne<AirportReview>()
            //        .WithMany()
            //        .HasForeignKey(f => f.DepartureAirportReviewId)
            //        .IsRequired(false)
            //        .HasConstraintName("FK_Flight_DepartureAirportReview");

            //    entity
            //        .HasOne<AirportReview>()
            //        .WithMany()
            //        .HasForeignKey(f => f.ArrivalAirportReviewId)
            //        .IsRequired(false)
            //        .HasConstraintName("FK_Flight_ArrivalAirportReview");

            //    entity
            //        .HasOne<AirlineReview>()
            //        .WithMany()
            //        .HasForeignKey(f => f.AirlineReviewId)
            //        .IsRequired(false)
            //        .HasConstraintName("FK_Flight_AirlineReview");

            //    entity
            //        .HasOne<AircraftReview>()
            //        .WithMany()
            //        .HasForeignKey(f => f.AircraftReviewId)
            //        .IsRequired(false)
            //        .HasConstraintName("FK_Flight_AircraftReview");
            //});


            //modelBuilder.Entity<AirlineReview>(entity =>
            //{
            //    entity.Property(r => r.Rating)
            //        .IsRequired();

            //    entity.Property(r => r.Comment)
            //        .HasMaxLength(200);

            //    entity
            //        .HasOne<User>()
            //        .WithMany()
            //        .HasForeignKey(r => r.ReviewerId)
            //        .IsRequired();

            //    entity
            //        .HasOne<Airline>()
            //        .WithMany()
            //        .HasForeignKey(r => r.AirlineId)
            //        .IsRequired();
            //});

            //modelBuilder.Entity<AirportReview>(entity =>
            //{
            //    entity.Property(r => r.Rating)
            //        .IsRequired();

            //    entity.Property(r => r.Comment)
            //        .HasMaxLength(200);

            //    entity
            //        .HasOne<User>()
            //        .WithMany()
            //        .HasForeignKey(r => r.ReviewerId)
            //        .IsRequired();

            //    entity
            //        .HasOne<Airport>()
            //        .WithMany()
            //        .HasForeignKey(r => r.AirportId)
            //        .IsRequired();
            //});
        }
    }
}

