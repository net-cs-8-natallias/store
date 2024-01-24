using Microsoft.EntityFrameworkCore;
using Order.Host.DbContextData;
using Order.Host.DbContextData.Entities;
using Order.Host.Repositories.Interfaces;

namespace Order.Host.Repositories;

public class OrderItemRepository: IOrderRepository<OrderItem>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<OrderItemRepository> _logger;

    public OrderItemRepository(ApplicationDbContext dbContext,
        ILogger<OrderItemRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    
    public async Task<List<OrderItem>> GetItems()
    {
        return await _dbContext.OrderItems.ToListAsync();
    }

    public async Task<OrderItem> FindById(int id)
    {
        var orderItem = await _dbContext.OrderItems.FindAsync(id);
        if (orderItem == null)
        {
            _logger.LogError($"*{GetType().Name}* order item with id: {id} does not exist");
            throw new Exception($"Order Item with ID: {id} does not exist");
        }

        return orderItem;
    }

    public async Task<int?> AddItem(OrderItem item)
    {
        await FindOrder(item.OrderId);
        var newOrderItem = await _dbContext.OrderItems.AddAsync(item);
        return newOrderItem.Entity.Id;
    }

    private async Task<CatalogOrder> FindOrder(int id)
    {
        var order = await _dbContext.CatalogOrders.FindAsync(id);
        if (order == null)
        {
            _logger.LogError($"*{GetType().Name}* order with id: {id} does not exist");
            throw new Exception($"Order with ID: {id} does not exist");
        }

        return order;
    }

    public async Task<OrderItem> UpdateItem(OrderItem item)
    {
        var order = await FindOrder(item.OrderId);
        var newItem = await FindById(item.Id);
        newItem.OrderId = order.Id;
        newItem.Quantity = item.Quantity;
        newItem.ItemId = item.ItemId;
        newItem.SubPrice = item.SubPrice;
        newItem = _dbContext.OrderItems.Update(newItem).Entity;
        await _dbContext.SaveChangesAsync();
        return newItem;

    }

    public async Task<OrderItem> RemoveItem(int id)
    {
        var item = await FindById(id);
        _dbContext.OrderItems.Remove(item);
        await _dbContext.SaveChangesAsync();
        return item;
    }
}