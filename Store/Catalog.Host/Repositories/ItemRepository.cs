using Catalog.Host.DbContextData;
using Catalog.Host.DbContextData.Entities;
using Catalog.Host.Models;
using Catalog.Host.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories;

public class ItemRepository: IItemRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<ItemRepository> _logger;

    public ItemRepository(ApplicationDbContext dbContext,
        ILogger<ItemRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    
    public async Task UpdateItemsStock(List<OrderItem> items)
    {
        foreach (var item in items)
        {
            var updatedItem = await _dbContext.Items.FindAsync(item.ItemId);
            if (updatedItem == null)
            {
                throw new Exception($"Item with id: {item.ItemId} does not exist");
            }

            int quantity = updatedItem.Quantity - item.Quantity;
            if (quantity < 0)
            {
                throw new Exception($"Not enough items in stock");
            }

            updatedItem.Quantity = quantity;
            _dbContext.Update(updatedItem);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<List<Item>> GetItemsByCatalogItemId(int catalogItemId)
    {
        IQueryable<Item> query = _dbContext.Items;
        query = query.Where(w => w.CatalogItemId == catalogItemId);
        return await query.ToListAsync();
    }
    
    public async Task<List<Item>> GetCatalog()
    {
        return await _dbContext.Items.ToListAsync();
    }

    public async Task<Item> FindById(int id)
    {
        var item = await _dbContext.Items
            .Include(i => i.CatalogItem)
            .FirstOrDefaultAsync(i => i.Id == id);
        
        if (item == null)
        {
            _logger.LogError($"*{GetType().Name}* item with id: {id} does not exist");
            throw new Exception($"Item with ID: {id} does not exist");
        }

        return item;
    }

    public async Task<int?> AddToCatalog(Item item)
    {
        await FindCatalogItem(item.CatalogItemId);
        var newItem = await _dbContext.Items.AddAsync(item);
        await _dbContext.SaveChangesAsync();
        return newItem.Entity.Id;
    }
    
    private async Task<CatalogItem> FindCatalogItem(int id)
    {
        var catalogItem = await _dbContext.CatalogItems.FindAsync(id);
        if (catalogItem == null)
        {
            _logger.LogError($"*{GetType().Name}* catalog item with id: {id} does not exist");
            throw new Exception($"Catalog Item with ID: {id} does not exist");
        }

        return catalogItem;
    }

    public async Task<Item> UpdateInCatalog(Item item)
    {
        var categoryItem = await FindCatalogItem(item.CatalogItemId);
        var newItem = await FindById(item.Id);
        
        newItem.CatalogItemId = categoryItem.Id;
        newItem.Quantity = item.Quantity;
        newItem.Size = item.Size;
        newItem = _dbContext.Items.Update(newItem).Entity;
        await _dbContext.SaveChangesAsync();
        return newItem;
    }

    public async Task<Item> RemoveFromCatalog(int id)
    {
        var item = await FindById(id);
        _dbContext.Items.Remove(item);
        await _dbContext.SaveChangesAsync();
        return item;
    }
}