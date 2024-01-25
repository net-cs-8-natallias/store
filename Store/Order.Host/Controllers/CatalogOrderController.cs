using Microsoft.AspNetCore.Mvc;
using Order.Host.DbContextData.Entities;
using Order.Host.Services.Interfaces;

namespace Order.Host.Controllers;

[ApiController]
[Route("catalog-order-controller")]
public class CatalogOrderController: ControllerBase
{
    private readonly ILogger<CatalogOrderController> _logger;
    private readonly IOrderService<CatalogOrder> _service;

    public CatalogOrderController(ILogger<CatalogOrderController> logger,
        IOrderService<CatalogOrder> service)
    {
        _logger = logger;
        _service = service;
    } 
}