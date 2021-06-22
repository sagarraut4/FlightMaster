using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightBookingSystem.Common.Helpers
{
    public interface IHttpHelper
    {
        Task<T> Get<T>(string url, IDictionary<string, string> headers);
        Task<T> Post<T>(string url, string content, IDictionary<string, string> headers);
    }
}
