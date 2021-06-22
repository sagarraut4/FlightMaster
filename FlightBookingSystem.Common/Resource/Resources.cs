using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;

namespace FlightBookingSystem.Common.Resource
{
    public interface IResource
    {
        string GetString(string key, string replacementKey = null);
    }
    
    public class Resources : IResource
    {
        private readonly JObject _resource;
        public Resources()
        {
            _resource = (JObject)JsonConvert.DeserializeObject(File.ReadAllText("Resource\\resource.json"));
        }
        public string GetString(string key, string replacementKey = null)
        {
            var retVal = _resource["root"].SelectToken(key)?.Value<string>();
            return string.IsNullOrWhiteSpace(replacementKey)?  retVal : retVal.Replace("{0}", replacementKey);
        }
    }
}
