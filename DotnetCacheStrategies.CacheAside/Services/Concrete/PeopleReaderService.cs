using DotnetCacheStrategies.CacheAside.Entities;
using DotnetCacheStrategies.CacheAside.Repositories.Abstract;
using DotnetCacheStrategies.CacheAside.Services.Abstract;

namespace DotnetCacheStrategies.CacheAside.Services.Concrete;

public class PeopleReaderService(
IReaderRepository<Person> repository) : IPeopleReaderService
{
    public async Task<IEnumerable<Person>> GetAllPeopleByName(string name)
    {
        return await repository.GetAll(x => x.Name == name);
    }
}