// Ignore Spelling: Redis

using System.Threading.Tasks;

namespace RedisCache.Contracts
{
    public interface IRedisCacheService
    {
        /// <summary>
        /// Set key to hold the string value. If key already holds a value, it is overwritten, regardless of its type.
        /// </summary>
        /// <typeparam name="T">Value type is generic</typeparam>
        /// <param name="key">The key of the string.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="TTL">Time-To-Live,kindly set the value in Milliseconds</param>
        /// <returns></returns>
        Task<bool> CreateAsync<T>(string key, T value, int TTL = default);
        /// <summary>
        /// Removes the specified key. A key is ignored if it does not exist.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> GetAsync<T>(string key);
        /// <summary>
        /// Get the value of key. If the key does not exist the special value nil is returned.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key of the string.</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(string key);
    }
}
