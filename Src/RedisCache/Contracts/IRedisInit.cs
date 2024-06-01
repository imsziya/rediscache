// Ignore Spelling: Redis

using StackExchange.Redis;

namespace RedisCache.Contracts
{
    public interface IRedisInit
    {
        IDatabase Init();
    }
}