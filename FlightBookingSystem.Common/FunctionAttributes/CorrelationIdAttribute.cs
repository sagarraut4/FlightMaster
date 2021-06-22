using FlightBookingSystem.Common.Constant;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FlightBookingSystem.Common.FunctionAttributes
{
    public class CorrelationIdAttribute : FunctionInvocationFilterAttribute
    {      
        public override Task OnExecutingAsync(FunctionExecutingContext executingContext, CancellationToken cancellationToken)
        {
            var request = executingContext.Arguments.First().Value as Microsoft.AspNetCore.Http.HttpRequest;
            try
            {
                StringValues correlationIds;
                request.Headers.TryGetValue(CommonConstants.CorrelationIdHeader, out correlationIds);
                var correlationId = correlationIds.FirstOrDefault();
                correlationId = correlationId ?? Guid.NewGuid().ToString();
                request.Headers.Add(CommonConstants.CorrelationIdHeader, correlationId);

                //this is required to add correlation id in header when response is sent to some other azure function
                request.HttpContext.Response.Headers.Add(CommonConstants.CorrelationIdHeader, correlationId);
                
            }
            catch(System.Exception e)
            {
                throw e;
            }
                return base.OnExecutingAsync(executingContext, cancellationToken);
        }
    }
}
