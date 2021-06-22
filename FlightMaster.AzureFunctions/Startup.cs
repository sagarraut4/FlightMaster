using FlightBookingSystem.Swagger;
using FlightMaster.Application;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(FlightMaster.AzureFunctions.Startup))]
namespace FlightMaster.AzureFunctions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddApplicationDependencies();
            builder.AddSwaggerDependencies();
        }
    }
}

