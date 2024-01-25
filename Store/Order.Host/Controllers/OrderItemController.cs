using Microsoft.AspNetCore.Mvc;
using Order.Host.DbContextData.Entities;
using Order.Host.Services.Interfaces;

namespace Order.Host.Controllers;

[ApiController]
[Route("order-item-controller")]
public class OrderItemController: ControllerBase
{
    private readonly ILogger<OrderItemController> _logger;
    private readonly IOrderService<OrderItem> _service;

    public OrderItemController(ILogger<OrderItemController> logger,
        IOrderService<OrderItem> service)
    {
        _logger = logger;
        _service = service;
    } 
}