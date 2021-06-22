using System;
using System.Collections.Generic;
using System.Text;

namespace FlightBookingSystem.Common.Logger
{
    public interface ILogMessage
    {
        void LogMessages(string correlationId, string messageType, string message);
    }

    public abstract class AbstractLogMessage : ILogMessage
    {
        public abstract void LogMessages(string correlationId, string messageType, string message);
        
    }
}
