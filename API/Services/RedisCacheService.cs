using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Sitanshu.Blogs.API.Services
{
    /// <summary>
    /// Reids Caching Service
    /// </summary>
    public class RedisCacheService
    {
        private readonly ILogger<RedisCacheService> _logger;
        private readonly IDistributedCache? _cache;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="cache"></param>
        public RedisCacheService(
            ILogger<RedisCacheService> logger,
            IDistributedCache cache)
        {
            _logger = logger;
            _cache = cache;
        }

        /// <summary>
        /// Get Cached information.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T? GetCachedData<T>(string key)
        {
            if (_cache == null)
            {
                _logger.LogInformation("Caching was not set up properly.");
                return default;
            }

            var jsonData = _cache.GetString(key);

            if (jsonData == null)
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(jsonData);
        }

        /// <summary>
        /// Set Caching information.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="cacheDuration"></param>
        public void SetCachedData<T>(string key, T data, TimeSpan cacheDuration)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = cacheDuration
            };

            var jsonData = JsonSerializer.Serialize(data);

            if (_cache == null)
            {
                _logger.LogInformation("Caching was not set up properly.");
                return;
            }

            _cache.SetString(key, jsonData, options);
        }
    }
}
