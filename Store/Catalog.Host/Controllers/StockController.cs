using Catalog.Host.DbContextData.Entities;
using Catalog.Host.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers;

[ApiController]
[Route("stocks")]
public class StockController: ControllerBase
{
    private readonly ILogger<StockController> _logger;
    private readonly ICatalogService<Item> _service;

    public StockController(ICatalogService<Item> service,
        ILogger<StockController> logger)
    {
        _service = service;
        _logger = logger;
    }
}