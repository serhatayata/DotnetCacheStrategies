using DotnetCacheStrategies.CacheAside.Entities;
using DotnetCacheStrategies.CacheAside.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCacheStrategies.CacheAside.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonController : ControllerBase
{
    private readonly IPeopleReaderService _peopleReaderService;
    private readonly IPeopleWriterService _peopleWriterService;

    public PersonController(
    IPeopleReaderService peopleReaderService, 
    IPeopleWriterService peopleWriterService)
    {
        _peopleReaderService = peopleReaderService;
        _peopleWriterService = peopleWriterService;
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll([FromQuery] string search)
    {
        var data = await _peopleReaderService.GetAllPeopleByName(search);
        return Ok(data);
    }

    [HttpPost("add")]
    public IActionResult Add([FromBody] Person person)
    {
        var data = _peopleWriterService.AddNewPerson(person.Name, person.Surname);
        return Ok(data);
    }
}
