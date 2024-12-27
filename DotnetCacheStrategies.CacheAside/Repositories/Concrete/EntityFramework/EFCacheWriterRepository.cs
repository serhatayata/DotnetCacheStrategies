using DotnetCacheStrategies.CacheAside.Data;
using DotnetCacheStrategies.CacheAside.Repositories.Abstract;
using DotnetCacheStrategies.CacheAside.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DotnetCacheStrategies.CacheAside.Repositories.Concrete.EntityFramework;

public class EFCacheWriterRepository<T> : EFWriterRepository<T>, IWriterRepository<T> where T : class
{
    ICacheProvider _cacheProvider;

    public EFCacheWriterRepository(AppDbContext context, ICacheProvider cacheProvider) : base(context)
    {
        _cacheProvider = cacheProvider;
    }

    public override void Add(T item)
    {
        base.Add(item);
        if (_currentTransaction == null || _currentTransaction.Completed)
            ClearCache();
    }

    private void ClearCache() => _cacheProvider.RemoveByPattern($"{typeof(T).Name}:*");

    public override ITransaction BeginTransaction(Action? transactionCommited = null) =>
        base.BeginTransaction(() =>
        {
            ClearCache();
            transactionCommited?.Invoke();
        });

    public override void Remove(T item)
    {
        base.Remove(item);
        if (_currentTransaction == null || _currentTransaction.Completed)
            ClearCache();
    }

    public override void Update(T item)
    {
        base.Update(item);
        if (_currentTransaction == null || _currentTransaction.Completed)
            ClearCache();
    }
}