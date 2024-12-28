using DotnetCacheStrategies.ReadThrough.Data;
using DotnetCacheStrategies.ReadThrough.Entities;

namespace DotnetCacheStrategies.ReadThrough;

public class ReadThroughCacheService
{
    private readonly RedisCacheService _cacheService;
    private readonly FakeDatabase _database;

    public ReadThroughCacheService(RedisCacheService cacheService, FakeDatabase database)
    {
        _cacheService = cacheService;
        _database = database;
    }

    public async Task<Product?> GetItemAsync(int id)
    {
        // Try to get the item from the cache
        var cachedItem = await _cacheService.GetCacheAsync<Product>(id.ToString());
        if (cachedItem != null)
        {
            return cachedItem;  // Return from cache if found
        }

        // If not in cache, get from the database and set it in the cache
        var itemFromDb = await _database.GetItemAsync(id);
        if (itemFromDb != null)
        {
            await _cacheService.SetCacheAsync(id.ToString(), itemFromDb);  // Cache the item for future use
        }

        return itemFromDb;
    }
}