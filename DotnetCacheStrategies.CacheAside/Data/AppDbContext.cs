using DotnetCacheStrategies.CacheAside.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotnetCacheStrategies.CacheAside.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(
    DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Person> People { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}
