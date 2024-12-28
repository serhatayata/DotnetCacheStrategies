using Microsoft.AspNetCore.Mvc;

namespace DotnetCacheStrategies.ReadThrough.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ReadThroughCacheService _cacheService;

    public ProductController(ReadThroughCacheService cacheService)
    {
        _cacheService = cacheService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var item = await _cacheService.GetItemAsync(id);
        return item == null ? NotFound() : Ok(item);
    }
}
