using FlightSaverApi.Models;
using FlightSaverApi.Models.Review;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlightSaverApi.Data.Configuration;

public class FlightConfiguration : IEntityTypeConfiguration<Flight>
{
    public void Configure(EntityTypeBuilder<Flight> builder)
    {
        builder.Property(f => f.FlightNumber)
                    .IsRequired(false)
                    .HasMaxLength(50);
                
        builder.Property(f => f.DepartureTime)
            .IsRequired();
        
        builder.Property(f => f.ArrivalTime)
            .IsRequired();
        
        builder.Property(f => f.ClassType)
            .IsRequired();
        
        builder.Property(f => f.SeatType)
            .IsRequired();
        
        builder.Property(f => f.SeatNumber)
            .IsRequired();
        
        builder.Property(f => f.Reason)
            .IsRequired();
        
        builder
            .HasOne(f => f.DepartureAirport)
            .WithMany(a => a.DepartingFlights)
            .HasForeignKey(f => f.DepartureAirportId)
            .IsRequired();
        
        builder
            .HasOne(f => f.ArrivalAirport)
            .WithMany(a => a.ArrivingFlights)
            .HasForeignKey(f => f.ArrivalAirportId)
            .IsRequired();
        
        builder
            .HasOne(f => f.Airline)
            .WithMany(a => a.Flights)
            .HasForeignKey(f => f.AirlineId)
            .IsRequired(false);
        
        builder
            .HasOne(f => f.Aircraft)
            .WithMany(a => a.Flights)
            .HasForeignKey(f => f.AircraftId)
            .IsRequired(false);

        builder.HasMany(f => f.AirportReviews)
            .WithOne(r => r.Flight)
            .HasForeignKey(r => r.FlightId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .HasOne(f => f.AircraftReview)
            .WithOne(f => f.Flight)
            .HasForeignKey<AircraftReview>(f => f.FlightId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .HasOne(f => f.AirlineReview)
            .WithOne(f => f.Flight)
            .HasForeignKey<AirlineReview>(f => f.FlightId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(f => f.User)
            .WithMany(u => u.Flights)
            .HasForeignKey(f => f.UserId)
            .IsRequired();
    }
}