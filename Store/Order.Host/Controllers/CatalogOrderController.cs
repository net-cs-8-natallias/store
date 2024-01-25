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

    [HttpGet("orders")]
    public async Task<IActionResult> GetOrders()
    {
        var orders = await _service.GetItems();
        return Ok(orders);
    }
    
    [HttpGet("orders/{id}")]
    public async Task<IActionResult> FindById(int id)
    {
        var order = await _service.FindById(id);
        return Ok(order);
    }
    
    [HttpPost("orders")]
    public async Task<IActionResult> AddOrder(CatalogOrder order)
    {
        var orderId = await _service.AddItem(order);
        return Ok(orderId);
    }
    
    [HttpPut("orders")]
    public async Task<IActionResult> UpdateOrder(CatalogOrder catalogOrder)
    {
        var order = await _service.UpdateItem(catalogOrder);
        return Ok(order);
    }
    
    [HttpDelete("orders/{id}")]
    public async Task<IActionResult> GetItems(int id)
    {
        var order = await _service.RemoveItem(id);
        return Ok(order);
    }

}