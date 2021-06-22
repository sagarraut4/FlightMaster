using AzureFunctions.Extensions.Swashbuckle;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using System.Reflection;

namespace FlightBookingSystem.Swagger
{
    public static class SwaggerDependency
    {
        public static IFunctionsHostBuilder AddSwaggerDependencies(this IFunctionsHostBuilder builder)
        {
            builder.AddSwashBuckle(Assembly.GetExecutingAssembly());
            return builder;
        }
    }
}
