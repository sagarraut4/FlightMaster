using AzureFunctions.Extensions.Swashbuckle;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Reflection;

namespace FlightBookingSystem.Swagger
{
    public static class SwaggerDependency
    {
        public static IFunctionsHostBuilder AddSwaggerDependencies(this IFunctionsHostBuilder builder)
        {
            builder.AddSwashBuckle(Assembly.GetExecutingAssembly(), opts =>
              opts.ConfigureSwaggerGen = (x =>
              {
                  x.CustomOperationIds(apiDesc =>
                  {
                      return apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)
                          ? methodInfo.Name
                          : new Guid().ToString();
                  });

                  x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                  {
                      Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer XXXXX')",
                      Name = "Authorization",
                      In = ParameterLocation.Header,
                      Type = SecuritySchemeType.ApiKey,
                      Scheme = "Bearer"
                  });

                  x.AddSecurityRequirement(new OpenApiSecurityRequirement
                      {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                    }
                                },
                                Array.Empty<string>()
                            }
                      });
              }));

            return builder;
        }
    }
}
