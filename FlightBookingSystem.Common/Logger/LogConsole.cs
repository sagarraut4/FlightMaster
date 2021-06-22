using System;
using System.Collections.Generic;
using System.Text;

namespace FlightBookingSystem.Common.Logger
{
    public class LogConsole : AbstractLogMessage
    {
        public override void LogMessages(string correlationId, string messageType, string message)
        {
            Console.WriteLine("MessageType :" + messageType);
            Console.WriteLine("CorrelationId :" + correlationId);
            Console.WriteLine("Message :" + message);
        }
    }
}
