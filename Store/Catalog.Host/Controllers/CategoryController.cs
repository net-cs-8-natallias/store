using Catalog.Host.DbContextData.Entities;
using Catalog.Host.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers;

[ApiController]
[Route("categories")]
public class CategoryController: ControllerBase
{
    private readonly ILogger<CategoryController> _logger;
    private readonly ICatalogService<ItemCategory> _service;

    public CategoryController(ICatalogService<ItemCategory> service,
        ILogger<CategoryController> logger)
    {
        _service = service;
        _logger = logger;
    }
    
    [HttpGet("categories")]
    public async Task<ActionResult> GetCategories()
    {
        var categories = await _service.GetCatalog();
        return Ok(categories);
    }

    [HttpGet("categories/{id}")]
    public async Task<ActionResult> GetCategoryById(int id)
    {
        var category = await _service.FindById(id);
        return Ok(category);
    }

    [HttpPost("categories")]
    public async Task<ActionResult> AddCategory(ItemCategory category)
    {
        var categoryId = await _service.AddToCatalog(category);
        return Ok(categoryId);
    }

    [HttpPut("categories")]
    public async Task<ActionResult> UpdateCategory(ItemCategory category)
    {
        var updatedCategory = await _service.UpdateInCatalog(category);
        return Ok(updatedCategory);
    }

    [HttpDelete("categories/{id}")]
    public async Task<ActionResult> DeleteCategory(int id)
    {
        var category = await _service.RemoveFromCatalog(id);
        return Ok(category);
    }
}