﻿using Microsoft.EntityFrameworkCore;

namespace FlightSaverApi.Models.Plane
{
    public class AircraftContext : DbContext
    {
        public AircraftContext(DbContextOptions<AircraftContext> options) : base(options)
        {

        }

        public DbSet<Aircraft> Aircrafts { get; set; }
    }
}
