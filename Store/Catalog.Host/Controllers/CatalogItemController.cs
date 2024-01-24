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
}