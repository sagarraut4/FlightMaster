using FlightInventory.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightInventory.DataAccess
{
    public class FlightInventoryDbContext : DbContext
    {
        public FlightInventoryDbContext(DbContextOptions<FlightInventoryDbContext> options) : base(options)
        {

        }

        public DbSet<Inventory> Inventory { get; set; }
    }
}
