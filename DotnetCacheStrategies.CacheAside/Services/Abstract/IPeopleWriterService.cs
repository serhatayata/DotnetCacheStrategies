namespace DotnetCacheStrategies.CacheAside.Services.Abstract;

public interface IPeopleWriterService
{
    bool AddNewPerson(string name, string surname);
}
