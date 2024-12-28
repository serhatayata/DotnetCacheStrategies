using DotnetCacheStrategies.WriteThrough.Entities;

namespace DotnetCacheStrategies.WriteThrough.Data;

public interface ICache
{
    Task<Product?> GetItemAsync(int id);
    Task SetItemAsync(Product item);
}
