using Newtonsoft.Json;
using System;

namespace FlightBookingSystem.Common.Helpers
{
    public class EventDetails
    {
        public EventDetails()
        {
            Id = Guid.NewGuid().ToString();
            EventTime = DateTime.Now.ToString();
        }
        
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("subject")]
        public string Subject { get; set; }
        [JsonProperty("eventType")]
        public string EventType { get; set; }
        [JsonProperty("eventTime")]
        public string EventTime { get; set; }
        [JsonProperty("data")]
        public object Data { get; set; }

        [JsonProperty("message")]
        public object Message { get; set; }
    }
}
