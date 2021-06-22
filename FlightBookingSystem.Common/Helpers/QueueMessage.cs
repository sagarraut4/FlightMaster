using System;
using System.Collections.Generic;
using System.Text;

namespace FlightBookingSystem.Common.Helpers
{
    public class QueueMessage
    {
        //
        // Summary:
        //     The Id of the Message.
        public string MessageId { get; set; }
        //
        // Summary:
        //     This value is required to delete the Message. If deletion fails using this popreceipt
        //     then the message has been dequeued by another client.
        public string PopReceipt { get; set; }
        //
        // Summary:
        //     The content of the Message.
        public string Message { get; set; }     
    }
}
