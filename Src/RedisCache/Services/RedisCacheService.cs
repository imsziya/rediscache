// Ignore Spelling: Redis
using System;
using System.Text.Json;
using System.Threading.Tasks;
using RedisCache.Contracts;
using StackExchange.Redis;

namespace RedisCache.Services
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDatabase _cache;
        public RedisCacheService(IRedisInit redisInit)
        {
            _cache = redisInit.Init();
        }

        public async Task<bool> CreateAsync<T>(string key, T value, int TTL = default)
        {
            bool created;
            if (TTL == default)
                created = await _cache.StringSetAsync(key, JsonSerializer.Serialize(value));
            else
                created = await _cache.StringSetAsync(key, JsonSerializer.Serialize(value), TimeSpan.FromMilliseconds(TTL));
            return created;
        }

        public async Task<bool> DeleteAsync(string key)
        {
            if (!_cache.KeyExists(key)) return default;
            return await _cache.KeyDeleteAsync(key);
        }

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
