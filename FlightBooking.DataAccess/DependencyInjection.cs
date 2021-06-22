using FlightBooking.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FlightBooking.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDatabaseDependencies(this IServiceCollection services)
        {
            string connectionString = Environment.GetEnvironmentVariable("SqlConnectionString");
            services.AddDbContext<FlightBookingDbContext>(
            options => SqlServerDbContextOptionsExtensions.UseSqlServer(options,
            connectionString));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IBookingRepository), typeof(BookingRepository));
            services.AddScoped(typeof(ICustomerRepository), typeof(CustomerRepository));
            services.AddScoped(typeof(IPassengerRepository), typeof(PassengerRepository));
            return services;
        }
    }
}
