using FlightBookingSystem.Common.Constant;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;

namespace FlightBookingSystem.Common
{
    public interface ICorrelationInfo
    {
        string GetCorrelationId(HttpRequest req);
    }
    public class CorrelationInfo : ICorrelationInfo
    {
        public string GetCorrelationId(HttpRequest req)
        {
            StringValues correlationIds;
            req.Headers.TryGetValue(CommonConstants.CorrelationIdHeader, out correlationIds);
            return correlationIds.FirstOrDefault();         
        }
    }
}
