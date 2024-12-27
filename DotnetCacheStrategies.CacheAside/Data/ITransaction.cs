namespace DotnetCacheStrategies.CacheAside.Data;

public interface ITransaction : IDisposable
{
    /// <summary>
    /// Commits all pending operations inside the remote engine storage
    /// </summary>
    void Commit();
    /// <summary>
    /// Rollbacks all pendant operations.
    /// </summary>
    void Rollback();
}
