using FlightSaverApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlightSaverApi.Data.Configuration;

public class AircraftConfiguration : IEntityTypeConfiguration<Aircraft>
{
    public void Configure(EntityTypeBuilder<Aircraft> builder)
    {
        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.IataCode)
            .IsRequired()
            .HasMaxLength(3);

        builder.Property(a => a.IcaoCode)
            .IsRequired()
            .HasMaxLength(4);

        builder.HasIndex(a => a.RegNumber).IsUnique();
        builder.Property(a => a.RegNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(a => a.AircraftUrl)
            .HasMaxLength(255);

        builder
            .HasOne(a => a.Airline)
            .WithMany(a => a.Aircrafts)
            .HasForeignKey(a => a.AirlineId)
            .IsRequired();

        builder
            .HasMany(a => a.AircraftReviews)
            .WithOne(r => r.Aircraft)
            .HasForeignKey(r => r.AircraftId);
                
        builder
            .HasMany(a => a.Flights)
            .WithOne(f => f.Aircraft)
            .HasForeignKey(r => r.AircraftId);
    }
}