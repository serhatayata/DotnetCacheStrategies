using DotnetCacheStrategies.WriteThrough.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCacheStrategies.WriteThrough.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly WriteThroughCacheService _cacheService;

    public ProductController(
    WriteThroughCacheService cacheService)
    {
        _cacheService = cacheService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetItem(int id)
    {
        var item = await _cacheService.GetItemAsync(id);
        if (item == null)
        {
            return NotFound();
        }
        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> SaveItem([FromBody] Product item)
    {
        await _cacheService.SaveItemAsync(item);
        return Ok();
    }
}