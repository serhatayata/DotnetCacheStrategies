using StackExchange.Redis;
using System.Text.Json;

namespace DotnetCacheStrategies.WriteBehind.Data;

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
        Console.WriteLine($"[Cache] Set: {key}");
    }

    public async Task<T?> GetCacheAsync<T>(string key)
    {
        var db = _redis.GetDatabase();
        var cachedValue = await db.StringGetAsync(key);
        return cachedValue.HasValue ? JsonSerializer.Deserialize<T>(cachedValue) : default;
    }
}