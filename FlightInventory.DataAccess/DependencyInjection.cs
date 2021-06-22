using FlightInventory.DataAccess.Repository;
using FlightInventory.DataAccess.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FlightInventory.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDatabaseDependencies(this IServiceCollection services)
        {
            string connectionString = Environment.GetEnvironmentVariable("SqlConnectionString");
            services.AddDbContext<FlightInventoryDbContext>(
            options => SqlServerDbContextOptionsExtensions.UseSqlServer(options,
            connectionString));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            return services;
        }
    }
}
