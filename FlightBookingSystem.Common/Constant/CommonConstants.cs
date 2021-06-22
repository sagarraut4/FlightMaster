using System;
using System.Collections.Generic;
using System.Text;

namespace FlightBookingSystem.Common.Constant
{
    public class CommonConstants
    {
        public const string CorrelationIdHeader = "correlationId";
        public const string UnauthorizedRequest = "error.unauthorizedRequest";
        public const string ValidRequest = "info.validRequest";
        public const string StorageConnectionStringName = "AzureWebJobsStorage";
        public const string AuthorizationStatus = "AuthorizationStatus";       
        public const string AppInsights = "AppInsights";
        public const string AppInsightInstrumentionKey = "APPINSIGHTS_INSTRUMENTATIONKEY";
        public const string RedisConnectionString = "RedisConnectionString";
        public const string Logging = "Logging";
    }
}
