using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order.Host.Models;
using Order.Host.Services.Interfaces;

namespace Order.Host.Controllers;


// [Authorize(Policy = "ApiScope")]
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
    
    [HttpPost("orders")]
    public async Task<IActionResult> CreateOrder(List<ItemModel> items, string userId = null)
    {
        if (userId == null)
        {
            userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
        var orderId = await _service.CreateOrder(items, userId!);
        return Ok(orderId);
    }

    [HttpGet("orders")]
    public async Task<IActionResult> GetOrders(string userId = null)
    {
        if (userId == null)
        {
            userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
        var orders = await _service.GetOrdersByUserId(userId!);
        return Ok(orders);
    }
}