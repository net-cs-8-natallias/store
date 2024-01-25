using Catalog.Host.DbContextData.Entities;
using Catalog.Host.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers;

[ApiController]
[Route("catalog-items")]
public class CatalogItemController: ControllerBase
{
    private readonly ILogger<CatalogItemController> _logger;
    private readonly ICatalogService<CatalogItem> _service;

    public CatalogItemController(
        ICatalogService<CatalogItem> service,
        ILogger<CatalogItemController> logger)
    {
        _service = service;
        _logger = logger;
    }
    
    [HttpGet("catalog-items")]
    public async Task<ActionResult> GetItems()
    {
        var items = await _service.GetCatalog();
        return Ok(items);
    }

    [HttpGet("catalog-items/{id}")]
    public async Task<ActionResult> GetItemById(int id)
    {
        var item = await _service.FindById(id);
        return Ok(item);
    }

    [HttpPost("catalog-items")]
    public async Task<ActionResult> AddItem(CatalogItem item)
    {
        var itemsId = await _service.AddToCatalog(item);
        return Ok(itemsId);
    }

    [HttpPut("catalog-items")]
    public async Task<ActionResult> UpdateItem(CatalogItem item)
    {
        var updatedItems = await _service.UpdateInCatalog(item);
        return Ok(updatedItems);
    }

    [HttpDelete("catalog-items/{id}")]
    public async Task<ActionResult> DeleteItem(int id)
    {
        var items = await _service.RemoveFromCatalog(id);
        return Ok(items);
    }
}