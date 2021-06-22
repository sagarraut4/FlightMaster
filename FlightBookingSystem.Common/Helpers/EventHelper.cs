using FlightBookingSystem.Common.Constant;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightBookingSystem.Common.Helpers
{
    public class EventHelper : IEventHelper
    {
        private readonly IHttpHelper _httpHelper;
        public EventHelper(IHttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }

        public async Task Publish(EventDetails eventDetails)
        {
            var headers = GetEventApiHeaders(eventDetails.EventType);
            var url = GetEventApiUrl(eventDetails.EventType);
            var events = new List<EventDetails>();
            events.Add(eventDetails);
            string content = JsonConvert.SerializeObject(events);
            await _httpHelper.Post<bool>(url, content, headers);
        }

        private IDictionary<string, string> GetEventApiHeaders(string eventType)
        {
            var header = new Dictionary<string, string>();
            switch(eventType)
            {
                case EventConstants.InventoryUpdate:
                    header.Add(Environment.GetEnvironmentVariable("UpdateInventoryEventKeyName"), Environment.GetEnvironmentVariable("UpdateInventoryEventKeyValue"));
                    break;
            }

            return header;
        }

        private string GetEventApiUrl(string eventType)
        {
            switch (eventType)
            {
                case EventConstants.InventoryUpdate:
                    return Environment.GetEnvironmentVariable("UpdateInventoryEventUrl");
            }
            return string.Empty;
        }
    }
}
