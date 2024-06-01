// Ignore Spelling: Redis

using Microsoft.Extensions.DependencyInjection;
using RedisCache.Contracts;
using RedisCache.Services;

namespace RedisCache.Extensions;

public static class AddDependencyExtension
{
    public static void AddRedisCache(this IServiceCollection services, string connStr)
    {
        services.AddScoped<IRedisInit>(opt => new RedisInit(connStr));
        services.AddScoped<IRedisCacheService, RedisCacheService>();
    }
}
