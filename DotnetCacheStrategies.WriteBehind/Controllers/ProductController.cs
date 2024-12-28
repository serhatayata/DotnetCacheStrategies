using DotnetCacheStrategies.WriteBehind.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCacheStrategies.WriteBehind.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly WriteBehindCacheService _cacheService;

    public ProductController(
    WriteBehindCacheService cacheService)
    {
        _cacheService = cacheService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var item = await _cacheService.GetItemAsync(id);
        return item == null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Set([FromBody] Product item)
    {
        await _cacheService.SetItemAsync(item);
        return Ok();
    }
}
