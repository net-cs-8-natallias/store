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
}