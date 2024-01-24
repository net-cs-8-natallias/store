using Catalog.Host.Data.Entities;
using Catalog.Host.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers;

[ApiController]
[Route("brands")]
public class BrandController: ControllerBase
{
    private readonly ILogger<BrandController> _logger;
    private readonly ICatalogService<ItemBrand> _service;

    public BrandController(ICatalogService<ItemBrand> service,
        ILogger<BrandController> logger)
    {
        _service = service;
        _logger = logger;
    }
}