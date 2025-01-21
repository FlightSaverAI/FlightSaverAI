using FlightSaverApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlightSaverApi.Data.Configuration;

public class AirportConfiguration : IEntityTypeConfiguration<Airport>
{
    public void Configure(EntityTypeBuilder<Airport> builder)
    {
        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(a => a.IataCode).IsUnique();
        builder.Property(a => a.IataCode)
            .IsRequired()
            .HasMaxLength(3);

        builder.HasIndex(a => a.IcaoCode).IsUnique();
        builder.Property(a => a.IcaoCode)
            .IsRequired()
            .HasMaxLength(4);

        builder.Property(a => a.City)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.Country)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.Latitude)
            .IsRequired()
            .HasColumnType("decimal(9, 6)");

        builder.Property(a => a.Longitude)
            .IsRequired()
            .HasColumnType("decimal(9, 6)");
            
        builder
            .HasMany(a => a.DepartingFlights)
            .WithOne(f => f.DepartureAirport)
            .HasForeignKey(f => f.DepartureAirportId);
            
        builder
            .HasMany(a => a.ArrivingFlights)
            .WithOne(f => f.ArrivalAirport)
            .HasForeignKey(r => r.ArrivalAirportId);
    }
}