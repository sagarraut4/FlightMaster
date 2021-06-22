using FlightBookingSystem.Common.Helpers;
using System;

namespace FlightBookingSystem.Common.Logger
{
    public class LogBlobMessage : AbstractLogMessage
    {
        const string fileType = ".txt";
        IBlobHelper _blobHelper;
        public LogBlobMessage(IBlobHelper blobHelper)
        {
            _blobHelper = blobHelper;
        }
        public override void LogMessages(string correlationId, string messageType, string message)
        {
            var blobFileName = string.Concat(DateTime.UtcNow.Year.ToString(), DateTime.UtcNow.Month.ToString(), DateTime.UtcNow.Day.ToString(), fileType);
            var messageLog = $"{DateTime.UtcNow.ToString() } [{messageType}] - correlationId : {correlationId} , {message} {Environment.NewLine}";
            _blobHelper.AddMessageInBlob(blobFileName, messageLog);
        }
    }    
}
