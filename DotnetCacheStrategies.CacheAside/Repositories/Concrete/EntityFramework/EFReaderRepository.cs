using System.Linq.Expressions;
using DotnetCacheStrategies.CacheAside.Data;
using DotnetCacheStrategies.CacheAside.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DotnetCacheStrategies.CacheAside.Repositories.Concrete.EntityFramework;

public class EFReaderRepository<T>(AppDbContext context) : IReaderRepository<T> where T : class
{
    public virtual async Task<bool> Any(Expression<Func<T, bool>> expression) => (await GetAll(expression))?.Any() ?? false;

    public virtual Task<List<T>> GetAll() => context.Set<T>().ToListAsync();

    public virtual Task<List<T>> GetAll(Expression<Func<T, bool>> expression) => context.Set<T>().Where(expression).ToListAsync();

    public virtual Task<T?> GetOne(Expression<Func<T, bool>> expression) => context.Set<T>().FirstOrDefaultAsync(expression);
}