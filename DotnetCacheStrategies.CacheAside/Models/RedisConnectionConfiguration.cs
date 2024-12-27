namespace DotnetCacheStrategies.CacheAside.Models;

public class RedisConnectionConfiguration
{
    public RedisConnectionConfiguration()
    {
    }

    public RedisConnectionConfiguration(
    string connectionString,
    int connectionTimeout)
    {
        this.ConnectionString = connectionString;
        this.ConnectionTimeout = connectionTimeout;
    }

    public string ConnectionString { get; set; }
    public int ConnectionTimeout { get; set; }
}
