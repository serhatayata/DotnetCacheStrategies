using DotnetCacheStrategies.WriteThrough.Entities;

namespace DotnetCacheStrategies.WriteThrough.Data;

public interface IDataStore
{
    Task<Product?> GetItemAsync(int id);
    Task SaveItemAsync(Product item);
}
