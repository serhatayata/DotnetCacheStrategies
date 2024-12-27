using DotnetCacheStrategies.CacheAside.Entities;

namespace DotnetCacheStrategies.CacheAside.Services.Abstract;

public interface IPeopleReaderService
{
    Task<IEnumerable<Person>> GetAllPeopleByName(string name);
}
