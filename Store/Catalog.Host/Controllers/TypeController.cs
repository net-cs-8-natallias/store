using Catalog.Host.DbContextData.Entities;
using Catalog.Host.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers;

[ApiController]
[Route("types")]
public class TypeController: ControllerBase
{
    private readonly ILogger<TypeController> _logger;
    private readonly ICatalogService<ItemType> _service;

    public TypeController(ICatalogService<ItemType> service,
        ILogger<TypeController> logger)
    {
        _service = service;
        _logger = logger;
    }
    
    [HttpGet("types")]
    public async Task<ActionResult> GetTypes()
    {
        var types = await _service.GetCatalog();
        return Ok(types);
    }

    [HttpGet("types/{id}")]
    public async Task<ActionResult> GetTypeById(int id)
    {
        var type = await _service.FindById(id);
        return Ok(type);
    }

    [HttpPost("types")]
    public async Task<ActionResult> AddType(ItemType type)
    {
        var typeId = await _service.AddToCatalog(type);
        return Ok(typeId);
    }

    [HttpPut("types")]
    public async Task<ActionResult> UpdateType(ItemType type)
    {
        var updatedType = await _service.UpdateInCatalog(type);
        return Ok(updatedType);
    }

    [HttpDelete("types/{id}")]
    public async Task<ActionResult> DeleteType(int id)
    {
        var type = await _service.RemoveFromCatalog(id);
        return Ok(type);
    }
}