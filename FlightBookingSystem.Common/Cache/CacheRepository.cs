using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
namespace FlightBookingSystem.Common.Cache
{
    public class CacheRepository : ICacheRepository
    {
        private readonly IDistributedCache _cache;
        private readonly DateTime _expiration = DateTime.Now.AddDays(Convert.ToInt32(Environment.GetEnvironmentVariable("RedisExpirationDays")));
        public CacheRepository(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task Add(string key, object value)
        {
            await _cache.SetStringAsync(key, JsonConvert.SerializeObject(value), new DistributedCacheEntryOptions() { AbsoluteExpiration = _expiration });
        }

        public async Task Delete(string key)
        {
            await _cache.RemoveAsync(key);
        }

        public async Task<T> Get<T>(string key)
        {
            string value = await _cache.GetStringAsync(key);
            if (string.IsNullOrEmpty(value))
                return default(T);

            return JsonConvert.DeserializeObject<T>(value);
        }

        public async Task<string> Get(string key)
        {
            return await _cache.GetStringAsync(key);
        }
    }
}
