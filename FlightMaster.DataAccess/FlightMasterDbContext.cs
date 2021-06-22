using FlightMaster.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightMaster.DataAccess
{
    public class FlightMasterDbContext : DbContext
    {
        public FlightMasterDbContext(DbContextOptions<FlightMasterDbContext> options) : base(options)
        {

        }

        public DbSet<CarrierType> CarrierTypes { get; set; }
        public DbSet<Airline> Airlines { get; set; }
        public DbSet<Aircraft> Aircrafts { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Airport> Airports { get; set; }
    }
}
