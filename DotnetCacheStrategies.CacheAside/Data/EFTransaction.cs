using Microsoft.EntityFrameworkCore.Storage;

namespace DotnetCacheStrategies.CacheAside.Data;

public class EFTransaction(IDbContextTransaction transaction, Action commitedCallback) : ITransaction, IDisposable
{
    public bool Completed { get; private set; } = false;

    public void Commit()
    {
        transaction.Commit();
        Completed = true;
        commitedCallback?.Invoke();
    }

    public void Dispose()
    {
        if (!Completed)
            Rollback();
    }

    public void Rollback()
    {
        transaction.Rollback();
        Completed = true;
    }
}