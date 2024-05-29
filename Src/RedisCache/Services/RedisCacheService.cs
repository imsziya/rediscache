// Ignore Spelling: Redis

using System;
using System.Text.Json;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace RedisCache.Services
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDatabase _cache;
        public RedisCacheService(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentNullException(connectionString);
            try
            {
                ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(connectionString);
                _cache = redis.GetDatabase();
            }
            catch (Exception)
            {
#pragma warning disable S112 // General or reserved exceptions should never be thrown
                throw new Exception("Redis Connection Failure. Kindly check whether redis server is alive");
#pragma warning restore S112 // General or reserved exceptions should never be thrown
            }
        }

        /// <summary>
        /// Set key to hold the string value. If key already holds a value, it is overwritten, regardless of its type.
        /// </summary>
        /// <typeparam name="T">Value type is generic</typeparam>
        /// <param name="key">The key of the string.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="TTL">Time-To-Live,kindly set the value in Milliseconds</param>
        /// <returns></returns>
        public async Task<bool> CreateAsync<T>(string key, T value, int TTL = default)
        {
            bool created;
            if (TTL == default)
                created = await _cache.StringSetAsync(key, JsonSerializer.Serialize(value));
            else
                created = await _cache.StringSetAsync(key, JsonSerializer.Serialize(value), TimeSpan.FromMilliseconds(TTL));
            return created;
        }

        /// <summary>
        /// Removes the specified key. A key is ignored if it does not exist.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> DeleteAsync(string key)
        {
            if (!_cache.KeyExists(key)) return default;
            return await _cache.KeyDeleteAsync(key);
        }

        /// <summary>
        /// Get the value of key. If the key does not exist the special value nil is returned.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key of the string.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<T> GetAsync<T>(string key)
        {
            if (!_cache.KeyExists(key)) return default;
            var data = await _cache.StringGetAsync(key);
            if (data.HasValue)
                return JsonSerializer.Deserialize<T>(data);
            return default;
        }
    }
}
