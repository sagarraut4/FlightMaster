using FlightInventory.Application;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(FlightInventory.AzureFunctions.Startup))]
namespace FlightInventory.AzureFunctions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddApplicationDependencies();
        }
    }
}
