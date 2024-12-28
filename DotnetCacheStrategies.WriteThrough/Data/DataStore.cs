using DotnetCacheStrategies.WriteThrough.Entities;

namespace DotnetCacheStrategies.WriteThrough.Data;

public class DataStore : IDataStore
{
    private readonly Dictionary<int, Product> _data = new();

    public Task<Product?> GetItemAsync(int id)
    {
        _data.TryGetValue(id, out var item);
        Console.WriteLine($"[Database] Retrieved: {id}");
        return Task.FromResult(item);
    }

    public Task SaveItemAsync(Product item)
    {
        _data[item.Id] = item;
        Console.WriteLine($"[Database] Saved: {item.Id} - {item.Name}");
        return Task.CompletedTask;
    }
}
