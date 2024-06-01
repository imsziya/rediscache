// Ignore Spelling: Redis


using System;
using RedisCache.Contracts;
using StackExchange.Redis;

namespace RedisCache.Services;

internal class RedisInit : IRedisInit
{
    private readonly string _connStr;
    public RedisInit(string connStr)
    {
        if (string.IsNullOrWhiteSpace(connStr)) throw new ArgumentNullException(connStr);
        _connStr = connStr;
    }
    public IDatabase Init()
    {
        try
        {
            return ConnectionMultiplexer.Connect(_connStr).GetDatabase();
        }
        catch (Exception)
        {
#pragma warning disable S112 // General or reserved exceptions should never be thrown
            throw new Exception("Redis Connection Failure. Kindly check whether redis server is alive");
#pragma warning restore S112 // General or reserved exceptions should never be thrown
        }
    }
}
