using System;
using FlightSaverApi.Models.Aircraft;
using FlightSaverApi.Models.User;
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

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Username).IsUnique();
                entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Role).IsRequired();
            });
        }
    }
}

