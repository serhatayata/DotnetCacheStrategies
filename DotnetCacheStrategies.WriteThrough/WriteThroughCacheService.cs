using DotnetCacheStrategies.WriteThrough.Data;
using DotnetCacheStrategies.WriteThrough.Entities;

namespace DotnetCacheStrategies.WriteThrough;

public class WriteThroughCacheService
{
    private readonly ICache _cache;
    private readonly IDataStore _dataStore;

    public WriteThroughCacheService(ICache cache, IDataStore dataStore)
    {
        _cache = cache;
        _dataStore = dataStore;
    }

    public async Task<Product?> GetItemAsync(int id)
    {
        // Try to get from cache
        var cachedItem = await _cache.GetItemAsync(id);
        if (cachedItem != null)
        {
            return cachedItem;
        }

        // Fallback to database
        var dbItem = await _dataStore.GetItemAsync(id);
        if (dbItem != null)
        {
            // Update the cache
            await _cache.SetItemAsync(dbItem);
        }

        return dbItem;
    }

    public async Task SaveItemAsync(Product item)
    {
        // Save to database
        await _dataStore.SaveItemAsync(item);

        // Write through to the cache
        await _cache.SetItemAsync(item);
    }
}
