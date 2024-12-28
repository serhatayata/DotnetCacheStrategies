using DotnetCacheStrategies.WriteBehind.Entities;

namespace DotnetCacheStrategies.WriteBehind.Data;

public class DataStore : IDataStore
{
    private readonly Dictionary<int, Product> _store = new();

    public Task SaveItemAsync(Product item)
    {
        _store[item.Id] = item;
        Console.WriteLine($"[Database] Saved: {item.Id} - {item.Name}");
        return Task.CompletedTask;
    }

    public Task<Product?> GetItemAsync(int id)
    {
        _store.TryGetValue(id, out var item);
        Console.WriteLine($"[Database] Retrieved: {id}");
        return Task.FromResult(item);
    }
}
