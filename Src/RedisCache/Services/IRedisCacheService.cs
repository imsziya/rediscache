// Ignore Spelling: Redis

using System.Threading.Tasks;

namespace RedisCache.Services
{
    public interface IRedisCacheService
    {
        Task<bool> CreateAsync<T>(string key, T value, int TTL = default);
        Task<T> GetAsync<T>(string key);
        Task<bool> DeleteAsync(string key);
    }
}
