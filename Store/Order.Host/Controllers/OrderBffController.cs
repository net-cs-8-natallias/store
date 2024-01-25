using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Order.Host.Services.Interfaces;

namespace Order.Host.Controllers;

[ApiController]
[Route("order-bff-controller")]
public class OrderBffController: ControllerBase
{
    private readonly ILogger<OrderBffController> _logger;
    private readonly IOrderBffService _service;

    public OrderBffController(ILogger<OrderBffController> logger,
        IOrderBffService service)
    {
        _logger = logger;
        _service = service;
    }  
    
    // Task<int?> CreateOrder(string userId);
    // Task<List<CatalogOrder>> GetOrdersByUserId(string userId);

    [HttpPost("orders")]
    public async Task<IActionResult> CreateOrder()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var orderId = await _service.CreateOrder(userId!);
        return Ok(orderId);
    }

    [HttpGet("orders")]
    public async Task<IActionResult> GetOrders()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var orders = await _service.GetOrdersByUserId(userId!);
        return Ok(orders);
    }
}