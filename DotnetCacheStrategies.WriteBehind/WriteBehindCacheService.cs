using System.Collections.Concurrent;
using DotnetCacheStrategies.WriteBehind.Data;
using DotnetCacheStrategies.WriteBehind.Entities;

namespace DotnetCacheStrategies.WriteBehind;

public class WriteBehindCacheService
{
    private readonly RedisCacheService _cacheService;
    private readonly IDataStore _database;
    private readonly ConcurrentQueue<Product> _writeQueue = new();
    private readonly CancellationTokenSource _cancellationTokenSource = new();

    public WriteBehindCacheService(
    RedisCacheService cacheService, 
    IDataStore database)
    {
        _cacheService = cacheService;
        _database = database;

        // Start background task to write to the database
        Task.Run(BackgroundDatabaseWriter, _cancellationTokenSource.Token);
    }

    public async Task<Product?> GetItemAsync(int id)
    {
        var cachedItem = await _cacheService.GetCacheAsync<Product>(id.ToString());
        return cachedItem;
    }

    public async Task SetItemAsync(Product item)
    {
        // Update cache immediately
        await _cacheService.SetCacheAsync(item.Id.ToString(), item);

        // Enqueue item for database update
        _writeQueue.Enqueue(item);
        Console.WriteLine($"[Write-Behind Queue] Enqueued: {item.Id}");
    }

    private async Task BackgroundDatabaseWriter()
    {
        while (!_cancellationTokenSource.Token.IsCancellationRequested)
        {
            while (_writeQueue.TryDequeue(out var item))
            {
                await _database.SaveItemAsync(item);
            }

            await Task.Delay(5000); // Process queue every 5 seconds
        }
    }

    public void Stop()
    {
        _cancellationTokenSource.Cancel();
    }
}
