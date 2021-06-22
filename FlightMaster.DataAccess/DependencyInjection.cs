using FlightMaster.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using FlightBookingSystem.Common.Helpers;
using System.Threading.Tasks;

namespace FlightMaster.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDatabaseDependencies(this IServiceCollection services)
        {
            string clientID = Environment.GetEnvironmentVariable("ClientID");
            string clientSecret = Environment.GetEnvironmentVariable("ClientSecret");
            string keyVaultName = Environment.GetEnvironmentVariable("KeyVaultName");;
            //Task<string> connection = Task.Run<string>(
            //                            async () => await AzureKeyVaultHelper.GetConnectionStringFromKeyVaultAsync(clientID, clientSecret, keyVaultName));

            //string connectionString = connection.Result;

            string connectionString = AzureKeyVaultHelper.GetConnectionString(clientID, clientSecret, keyVaultName);

            services.AddDbContext<FlightMasterDbContext>(
            options => SqlServerDbContextOptionsExtensions.UseSqlServer(options,
            connectionString));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IAircraftRepository), typeof(AircraftRepository));
            services.AddScoped(typeof(ICityRepository), typeof(CityRepository));
            return services;
        }
    }
}
