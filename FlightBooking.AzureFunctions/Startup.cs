using FlightBooking.Application;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(FlightBooking.AzureFunctions.Startup))]
namespace FlightBooking.AzureFunctions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddApplicationDependencies();
        }
    }
}
