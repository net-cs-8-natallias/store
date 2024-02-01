using Catalog.Host.DbContextData;
using Catalog.Host.DbContextData.Entities;
using Catalog.Host.Models;
using Catalog.Host.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories;

public class CatalogItemRepository: ICatalogItemRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<CatalogItemRepository> _logger;

    public CatalogItemRepository(ApplicationDbContext dbContext,
        ILogger<CatalogItemRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    
    public async Task<List<CatalogItem>> GetCatalog(CatalogFilter filter)
    {
        IQueryable<CatalogItem> query = _dbContext.CatalogItems;
        if (filter.Category > 0) query = query.Where(w => w.ItemCategoryId == filter.Category);
        if (filter.Type > 0) query = query.Where(w => w.ItemTypeId == filter.Type);
        if (filter.Brand > 0) query = query.Where(w => w.ItemBrandId == filter.Brand);

        return await query.ToListAsync();
    }

    public async Task<List<CatalogItem>> GetCatalog()
    {
        return await _dbContext.CatalogItems.ToListAsync();
    }

    public async Task<CatalogItem> FindById(int id)
    {
        var item = await _dbContext.CatalogItems.FindAsync(id);
        if (item == null)
        {
            _logger.LogError($"*{GetType().Name}* item with id: {id} does not exist");
            throw new Exception($"Item with ID: {id} does not exist");
        }

        return item;
    }

    public async Task<int?> AddToCatalog(CatalogItem item)
    {
        await FindBrand(item.ItemBrandId);
        await FindType(item.ItemTypeId);
        await FindCategory(item.ItemCategoryId);
        var newItem = await _dbContext.CatalogItems.AddAsync(item);
        await _dbContext.SaveChangesAsync();
        return newItem.Entity.Id;
    }
    private async Task<ItemBrand> FindBrand(int id)
    {
        var brand = await _dbContext.ItemBrands.FindAsync(id);
        if (brand == null)
        {
            _logger.LogError($"*{GetType().Name}* brand with id: {id} does not exist");
            throw new Exception($"Brand with ID: {id} does not exist");
        }

        return brand;
    }
    
    private async Task<ItemType> FindType(int id)
    {
        var type = await _dbContext.ItemTypes.FindAsync(id);
        if (type == null)
        {
            _logger.LogError($"*{GetType().Name}* type with id: {id} does not exist");
            throw new Exception($"Type with ID: {id} does not exist");
        }

        return type;
    }
    
    private async Task<ItemCategory> FindCategory(int id)
    {
        var category = await _dbContext.ItemCategories.FindAsync(id);
        if (category == null)
        {
            _logger.LogError($"*{GetType().Name}* category with id: {id} does not exist");
            throw new Exception($"Category with ID: {id} does not exist");
        }

        return category;
    }

    public async Task<CatalogItem> UpdateInCatalog(CatalogItem item)
    {
        var brand = await FindBrand(item.ItemBrandId);
        var type = await FindType(item.ItemTypeId);
        var category = await FindCategory(item.ItemCategoryId);
        var newItem = await FindById(item.Id);

        newItem.ItemBrandId = brand.Id;
        newItem.ItemTypeId = type.Id;
        newItem.ItemCategoryId = category.Id;
        newItem.Name = item.Name;
        newItem.Price = item.Price;
        newItem.Description = item.Description;
        newItem.Image = item.Image;
        newItem = _dbContext.CatalogItems.Update(newItem).Entity;
        await _dbContext.SaveChangesAsync();
        return newItem;
    }

    public async Task<CatalogItem> RemoveFromCatalog(int id)
    {
        var item = await FindById(id);
        _dbContext.CatalogItems.Remove(item);
        await _dbContext.SaveChangesAsync();
        return item;
    }
    
}