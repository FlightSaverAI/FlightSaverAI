using FlightSaverApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlightSaverApi.Data.Configuration;

public class AirlineConfiguration : IEntityTypeConfiguration<Airline>
{
    public void Configure(EntityTypeBuilder<Airline> builder)
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

            builder.Property(a => a.Country)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.LogoUrl)
                .HasMaxLength(255);

            builder
                .HasMany(a => a.Aircrafts)
                .WithOne(a => a.Airline)
                .HasForeignKey(a => a.AirlineId)
                .IsRequired();
                
            builder
                .HasMany(a => a.AirlineReviews)
                .WithOne(a => a.Airline)
                .HasForeignKey(r => r.AirlineId);
                
        builder
                .HasMany(a => a.Flights)
                .WithOne(a => a.Airline)
                .HasForeignKey(r => r.AirlineId);
    }
}