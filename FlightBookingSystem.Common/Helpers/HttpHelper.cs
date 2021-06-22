using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.Common.Helpers
{
    public class HttpHelper : IHttpHelper
    {
        public async Task<T> Get<T>(string url, IDictionary<string, string> headers)
        {
            var stringResponse = string.Empty;

            using (var httpClient = new HttpClient())
            {
                foreach (var header in headers)
                    httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);

                using (var response = await httpClient.GetAsync(new Uri(url)))
                {
                    stringResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return JsonConvert.DeserializeObject<T>(stringResponse);
        }

        public async Task<T> Post<T>(string url, string content, IDictionary<string, string> headers)
        {
            string stringResponse = string.Empty;

            using (var httpClient = new HttpClient())
            {
                StringContent body = new StringContent(content, Encoding.UTF8, "application/json");
                foreach (var header in headers)
                    httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);

                using (var response = await httpClient.PostAsync(new Uri(url), body))
                {
                    stringResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return JsonConvert.DeserializeObject<T>(stringResponse);
        }
    }
}
