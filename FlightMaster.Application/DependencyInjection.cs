using AutoMapper;
using FlightBookingSystem.Common;
using FlightMaster.DataAccess;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FlightMaster.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddDatabaseDependencies();
            services.AddCommonDependencies();            
            return services;
        }
    }
}
