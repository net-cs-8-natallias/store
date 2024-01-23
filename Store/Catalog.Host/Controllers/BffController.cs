using Catalog.Host.Models;
using Catalog.Host.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers;

[Authorize(Policy = "ApiScope")]
[ApiController]
[Route("bff-controller")]
public class BffController: ControllerBase
{
    private readonly ILogger<BffController> _logger;
    private readonly IBffService _service;

    public BffController(
        ILogger<BffController> logger,
        IBffService service
        )
    {
        _logger = logger;
        _service = service;
    }
    
    [HttpGet("items")]
    public async Task<IActionResult> GetItems(int category, int type, int brand)
    {
        _logger.LogInformation(
            $"*{GetType().Name}* request to get items");
        var catalogItems = await _service.GetItems(new CatalogFilter(){Category = category, Type = type, Brand = brand});
        return Ok(catalogItems);
    }

    [HttpPut("items")]
    public async Task<IActionResult> UpdateStock(List<OrderItem> items)
    {
        //TODO
        return Ok();
    }

    [HttpGet("brands")]
    public async Task<ActionResult> GetBrands()
    {
        _logger.LogInformation($"*{GetType().Name}* request to get all brands");
        var catalogBrands = await _service.GetBrands();
        return Ok(catalogBrands);
    }

    [HttpGet("types")]
    public async Task<ActionResult> GetTypes()
    {
        _logger.LogInformation($"*{GetType().Name}* request to get all types");
        var catalogTypes = await _service.GetTypes();
        return Ok(catalogTypes);
    }

    [HttpGet("items/{id:int}")]
    public async Task<ActionResult> GetItem(int id)
    {
        _logger.LogInformation($"*{GetType().Name}* request to get item by id: {id}");
        var item = await _service.GetItem(id);
        return Ok(item);
    }

    [HttpGet("brands/{id}")]
    public async Task<ActionResult> GetBrand(int id)
    {
        _logger.LogInformation($"*{GetType().Name}* request to get item by id: {id}");
        var item = await _service.GetBrand(id);
        return Ok(item);
    }

    [HttpGet("types/{id}")]
    public async Task<ActionResult> GetType(int id)
    {
        _logger.LogInformation($"*{GetType().Name}* request to get item by id: {id}");
        var item = await _service.GetType(id);
        return Ok(item);
    }
    
}