using Order.Host.DbContextData.Entities;
using Order.Host.Models;
using Order.Host.Repositories.Interfaces;
using Order.Host.Services.Interfaces;

namespace Order.Host.Services;

public class OrderBffService: IOrderBffService
{
    private readonly ICatalogOrderRepository _catalogOrderRepository;
    private readonly IOrderRepository<OrderItem> _orderItemRepository;
    private readonly ILogger<OrderBffService> _logger;

    public OrderBffService(ICatalogOrderRepository catalogOrderRepository,
        IOrderRepository<OrderItem> orderItemRepository,
        ILogger<OrderBffService> logger)
    {
        _catalogOrderRepository = catalogOrderRepository;
        _orderItemRepository = orderItemRepository;
        _logger = logger;
    }
    //_logger.LogInformation($"*{GetType().Name}* items in the basket: {string.Join(", ", orderItems)}");
    public async Task<int?> CreateOrder(List<OrderItemModel> items, string userId)
    {
        CatalogOrder order = new CatalogOrder() { UserId = userId, Date = DateTime.Now.ToShortDateString()};
        int? orderId = await _catalogOrderRepository.AddItem(order);
        _logger.LogInformation($"*{GetType().Name}* creating new order with id: {orderId}");
        decimal totalPrice = items.Sum(item => item.Quantity * item.Price);
        int totalQuantity = items.Sum(item => item.Quantity);
        foreach (var item in items)
        {
            var orderItem = await _orderItemRepository.AddItem(new()
            {
                OrderId = orderId.Value,
                ItemId = item.ItemId,
                Quantity = item.Quantity,
                SubPrice = item.Quantity * item.Price
            });
            _logger.LogInformation($"*{GetType().Name}* creating new order item: {orderItem.ToString()}");
        }
        order.Id = orderId.Value;
        order.TotalPrice = totalPrice;
        order.TotalQuantity = totalQuantity;
        var newOrder = await _catalogOrderRepository.UpdateItem(order);
        _logger.LogInformation($"*{GetType().Name}* new order was added: {order.ToString()}");
        return orderId.Value;
    }

    public async Task<List<CatalogOrder>> GetOrdersByUserId(string userId)
    {
        var orders = await _catalogOrderRepository.GetOrdersByUserId(userId);
        _logger.LogInformation($"*{GetType().Name}* found orders {orders.Count}: {string.Join(", ", orders)}");
        return orders;
    }
}