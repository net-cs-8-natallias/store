using Catalog.Host.DbContextData.Entities;
using Catalog.Host.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers;

[ApiController]
[Route("items")]
public class ItemController: ControllerBase
{
    private readonly ILogger<ItemController> _logger;
    private readonly ICatalogService<Item> _service;

    public ItemController(ICatalogService<Item> service,
        ILogger<ItemController> logger)
    {
        _service = service;
        _logger = logger;
    }
}