using Microsoft.EntityFrameworkCore;
using Order.Host.DbContextData;
using Order.Host.DbContextData.Entities;
using Order.Host.Repositories.Interfaces;

namespace Order.Host.Repositories;

public class CatalogOrderRepository: ICatalogOrderRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<CatalogOrderRepository> _logger;

    public CatalogOrderRepository(ApplicationDbContext dbContext,
        ILogger<CatalogOrderRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    public async Task<List<CatalogOrder>> GetOrdersByUserId(string userId)
    {
        IQueryable<CatalogOrder> query = _dbContext.CatalogOrders;
        query = query.Where(item => item.UserId == userId);
        return await query.ToListAsync();
    }
    
    public async Task<List<CatalogOrder>> GetItems()
    {
        return await _dbContext.CatalogOrders.ToListAsync();
    }

    public async Task<CatalogOrder> FindById(int id)
    {
        var catalogOrder = await _dbContext.CatalogOrders.FindAsync(id);
        if (catalogOrder == null)
        {
            _logger.LogError($"*{GetType().Name}* order with id: {id} does not exist");
            throw new Exception($"Order with ID: {id} does not exist");
        }

        return catalogOrder;
    }

    public async Task<int?> AddItem(CatalogOrder item)
    {
        var newOrder = await _dbContext.CatalogOrders.AddAsync(item);
        await _dbContext.SaveChangesAsync();
        return newOrder.Entity.Id;
    }

    public async Task<CatalogOrder> UpdateItem(CatalogOrder item)
    {
        var newOrder = await FindById(item.Id);
        newOrder.Date = item.Date;
        newOrder.TotalPrice = item.TotalPrice;
        newOrder.TotalQuantity = item.TotalQuantity;
        newOrder.UserId = item.UserId;
        newOrder = _dbContext.CatalogOrders.Update(newOrder).Entity;
        await _dbContext.SaveChangesAsync();
        return newOrder;
    }

    public async Task<CatalogOrder> RemoveItem(int id)
    {
        var order = await FindById(id);
        IQueryable<OrderItem> query = _dbContext.OrderItems;
        query = query.Where(item => item.OrderId == order.Id);
        foreach (var i in await query.ToListAsync())
        {
            _dbContext.OrderItems.Remove(i);
            await _dbContext.SaveChangesAsync();
        }
        _dbContext.CatalogOrders.Remove(order);
        await _dbContext.SaveChangesAsync();
        return order;
    }
    
}