using System.Text.Json;
using DotnetCacheStrategies.WriteThrough.Entities;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace DotnetCacheStrategies.WriteThrough.Data;

public class RedisCacheService : ICache
{
    private readonly IConnectionMultiplexer _redisCache;

    public RedisCacheService(IConnectionMultiplexer redisCache)
    {
        _redisCache = redisCache;
    }

    public async Task<Product?> GetItemAsync(int id)
    {
        var database = _redisCache.GetDatabase();
        if (database == null)
            return null;


        var cachedItem = await database.StringGetAsync(id.ToString());
        if (cachedItem.HasValue)
        {
            Console.WriteLine($"[Redis Cache] Retrieved: {id}");
            return JsonSerializer.Deserialize<Product>(cachedItem);
        }

        Console.WriteLine($"[Redis Cache] Miss for: {id}");
        return null;
    }

    public async Task SetItemAsync(Product item)
    {
        var serializedItem = JsonSerializer.Serialize(item);

        var database = _redisCache.GetDatabase();
        if (database == null)
            return;

        await database.StringSetAsync(item.Id.ToString(), serializedItem, TimeSpan.FromMinutes(10));
        Console.WriteLine($"[Redis Cache] Set: {item.Id} - {item.Name}");
    }
}
