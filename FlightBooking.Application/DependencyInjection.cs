using AutoMapper;
using FlightBooking.DataAccess;
using FlightBookingSystem.Common;
using FlightBookingSystem.Common.Helpers;
using MassTransit;
using MediatR;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace FlightBooking.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddDatabaseDependencies();
            services.AddCommonDependencies();
            string connectionString = Environment.GetEnvironmentVariable("ServiceBusConnectionString");
            string topic = Environment.GetEnvironmentVariable("ServiceBusTopic");
            // Masstransit config
            var azureServiceBus = Bus.Factory.CreateUsingAzureServiceBus(busFactoryConfig =>
            {
                busFactoryConfig.Message<EventDetails>(configTopology =>
                {
                    configTopology.SetEntityName(topic);
                });

                busFactoryConfig.Host(connectionString, hostConfig =>
                {
                    // This is optional, but you can specify the protocol to use.
                    hostConfig.TransportType = TransportType.AmqpWebSockets;
                });

            });

            services.AddMassTransit(config =>
            {
                config.AddBus(provider => azureServiceBus);
            });

            services.AddSingleton<IPublishEndpoint>(azureServiceBus);
            services.AddSingleton<ISendEndpointProvider>(azureServiceBus);
            services.AddSingleton<IBus>(azureServiceBus);
            return services;
        }
    }
}
