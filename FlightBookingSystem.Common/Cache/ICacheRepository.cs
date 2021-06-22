using System.Threading.Tasks;

namespace FlightBookingSystem.Common.Cache
{
    public interface ICacheRepository
    {
        Task Add(string key, object value);
        Task<T> Get<T>(string key);
        Task<string> Get(string key);
        Task Delete(string key);
    }
}
