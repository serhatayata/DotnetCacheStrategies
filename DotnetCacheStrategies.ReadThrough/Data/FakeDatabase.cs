using DotnetCacheStrategies.ReadThrough.Entities;

namespace DotnetCacheStrategies.ReadThrough.Data;

public class FakeDatabase
{
    private readonly Dictionary<int, Product> _data = new()
    {
        { 1, new Product { Id = 1, Name = "Item 1" } },
        { 2, new Product { Id = 2, Name = "Item 2" } },
        { 3, new Product { Id = 3, Name = "Item 3" } }
    };

    public Task<Product?> GetItemAsync(int id)
    {
        _data.TryGetValue(id, out var item);
        return Task.FromResult(item);
    }
}