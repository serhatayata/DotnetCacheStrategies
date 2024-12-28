using DotnetCacheStrategies.WriteBehind.Entities;

namespace DotnetCacheStrategies.WriteBehind.Data;

public interface IDataStore
{
    Task<Product?> GetItemAsync(int id);
    Task SaveItemAsync(Product item);
}
