using System;
using FlightSaverApi.Models.Aircraft;
using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Data
{
    public class AircraftContext : DbContext
    {
        public AircraftContext(DbContextOptions<AircraftContext> options) : base(options)
        {

        }

        public DbSet<Aircraft> Aircrafts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aircraft>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.IataCode).HasMaxLength(3);
                entity.Property(e => e.IcaoCode).HasMaxLength(4);
                entity.Property(e => e.RegNumber).HasMaxLength(10);
            });
        }
    }
}

