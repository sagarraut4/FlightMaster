using Azure.Storage.Blobs.Specialized;
using FlightBookingSystem.Common.Cache;
using FlightBookingSystem.Common.Constant;
using FlightBookingSystem.Common.Helpers;
using FlightBookingSystem.Common.Logger;
using FlightBookingSystem.Common.Resource;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FlightBookingSystem.Common
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCommonDependencies(this IServiceCollection services)
        {
            var loggingType = Environment.GetEnvironmentVariable(CommonConstants.Logging).ToLower();
           
            services.AddDistributedRedisCache(
            options =>
            {
                options.Configuration = Environment.GetEnvironmentVariable(CommonConstants.RedisConnectionString);
            }
            );
            services.AddSingleton(typeof(ICacheRepository), typeof(CacheRepository));
            services.AddTransient(typeof(IHttpHelper), typeof(HttpHelper));
            services.AddTransient(typeof(IEventHelper), typeof(EventHelper));
            services.AddSingleton(typeof(IQueueHelper), typeof(QueueHelper));

            InjectLogServiceInstance(services, loggingType);
            services.AddSingleton(typeof(ICorrelationInfo), typeof(CorrelationInfo));
            return services;
        }

        public static void InjectLogServiceInstance(IServiceCollection services, string logType)
        { 
            switch (logType)
            {
                case "appinsight":
                    services.AddSingleton<TelemetryConfiguration>(sp =>
                    {
                        return new TelemetryConfiguration(Environment.GetEnvironmentVariable(CommonConstants.AppInsightInstrumentionKey));
                    });
                    services.AddSingleton(typeof(ILogMessage), typeof(LogTraces));
                    break;
                case "blob":
                    services.AddSingleton(typeof(IBlobHelper), typeof(BlobHelper));
                    services.AddSingleton(typeof(ILogMessage), typeof(LogBlobMessage));
                    break;
                case "console":
                    services.AddSingleton(typeof(ILogMessage), typeof(LogConsole));
                    break;
            }          
        }
    }
}
