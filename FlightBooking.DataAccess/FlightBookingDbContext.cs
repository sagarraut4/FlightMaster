using FlightBooking.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightBooking.DataAccess
{
    public class FlightBookingDbContext : DbContext
    {
        public FlightBookingDbContext(DbContextOptions<FlightBookingDbContext> options) : base(options)
        {

        }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
    }
}
