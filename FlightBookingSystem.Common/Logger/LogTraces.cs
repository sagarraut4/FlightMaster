using FlightBookingSystem.Common.Constant;
using FlightBookingSystem.Common.Enum;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using System;

namespace FlightBookingSystem.Common.Logger
{
    public class LogTraces : AbstractLogMessage
    {
        private readonly TelemetryClient _telemetryClient;

        public LogTraces(TelemetryConfiguration telemetryConfiguration)
        {
            _telemetryClient = new TelemetryClient(telemetryConfiguration);
        }
        public override void LogMessages(string correlationId, string messageType, string message)
        {
            var telemetry = new TraceTelemetry(Environment.GetEnvironmentVariable("TelemetryMessage"), messageType == LoggingType.Error.ToString() ? SeverityLevel.Error : SeverityLevel.Verbose);
            telemetry.Properties.Add(Environment.GetEnvironmentVariable("CorrelationId"), correlationId);
            telemetry.Properties.Add(Environment.GetEnvironmentVariable("Message"), message);
            telemetry.Properties.Add(Environment.GetEnvironmentVariable("MessageType"), messageType);
            _telemetryClient.TrackTrace(telemetry);
        }
    }
}
