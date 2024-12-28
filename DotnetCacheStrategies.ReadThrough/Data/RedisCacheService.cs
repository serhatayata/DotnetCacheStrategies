using System.Text.Json;
using StackExchange.Redis;

namespace DotnetCacheStrategies.ReadThrough.Data;

public class RedisCacheService
{
    private readonly IConnectionMultiplexer _redis;

    public RedisCacheService(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }

    public async Task SetCacheAsync<T>(string key, T value)
    {
        var db = _redis.GetDatabase();
        var serializedValue = JsonSerializer.Serialize(value);
        await db.StringSetAsync(key, serializedValue);
    }

    public async Task<T?> GetCacheAsync<T>(string key)
    {
        var db = _redis.GetDatabase();
        var cachedValue = await db.StringGetAsync(key);
        return cachedValue.HasValue ? JsonSerializer.Deserialize<T>(cachedValue) : default;
    }
}