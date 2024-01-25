using Catalog.Host.DbContextData;
using Catalog.Host.DbContextData.Entities;
using Catalog.Host.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories;

public class TypeRepository: ICatalogRepository<ItemType>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<TypeRepository> _logger;

    public TypeRepository(ApplicationDbContext dbContext,
        ILogger<TypeRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    
    public async Task<List<ItemType>> GetCatalog()
    {
        return await _dbContext.ItemTypes.ToListAsync();
    }

    public async Task<ItemType> FindById(int id)
    {
        var type = await _dbContext.ItemTypes.FindAsync(id);
        if (type == null)
        {
            _logger.LogError($"*{GetType().Name}* type with id: {id} does not exist");
            throw new Exception($"Type with ID: {id} does not exist");
        }

        return type;
    }

    public async Task<int?> AddToCatalog(ItemType item)
    {
        var newType = await _dbContext.ItemTypes.AddAsync(item);
        await _dbContext.SaveChangesAsync();
        return newType.Entity.Id;
    }

    public async Task<ItemType> UpdateInCatalog(ItemType item)
    {
        var newType = await FindById(item.Id);
        newType.Type = item.Type;
        newType = _dbContext.ItemTypes.Update(newType).Entity;
        await _dbContext.SaveChangesAsync();
        return newType;
    }

    public async Task<ItemType> RemoveFromCatalog(int id)
    {
        var type = await FindById(id);
        _dbContext.ItemTypes.Remove(type);
        await _dbContext.SaveChangesAsync();
        return type;
    }
}