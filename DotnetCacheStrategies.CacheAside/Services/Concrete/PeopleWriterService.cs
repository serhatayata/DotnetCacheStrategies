using DotnetCacheStrategies.CacheAside.Entities;
using DotnetCacheStrategies.CacheAside.Repositories.Abstract;
using DotnetCacheStrategies.CacheAside.Services.Abstract;

namespace DotnetCacheStrategies.CacheAside.Services.Concrete;

public class PeopleWriterService(
IWriterRepository<Person> repository) : IPeopleWriterService
{
    public bool AddNewPerson(string name, string surname)
    {
        repository.Add(new Person { Name = name, Surname = surname });
        return true;
    }
}
