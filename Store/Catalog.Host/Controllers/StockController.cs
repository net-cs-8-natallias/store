using Catalog.Host.DbContextData.Entities;
using Catalog.Host.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers;

[ApiController]
[Route("stocks")]
public class StockController: ControllerBase
{
    private readonly ILogger<StockController> _logger;
    private readonly ICatalogService<Stock> _service;

    public StockController(ICatalogService<Stock> service,
        ILogger<StockController> logger)
    {
        _service = service;
        _logger = logger;
    }
}