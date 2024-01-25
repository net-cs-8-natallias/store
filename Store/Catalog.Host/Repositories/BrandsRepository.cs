using Catalog.Host.DbContextData;
using Catalog.Host.DbContextData.Entities;
using Catalog.Host.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories;

public class BrandsRepository: ICatalogRepository<ItemBrand>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<BrandsRepository> _logger;

    public BrandsRepository(ApplicationDbContext dbContext,
        ILogger<BrandsRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    public async Task<List<ItemBrand>> GetCatalog()
    {
        return await _dbContext.ItemBrands.ToListAsync();
    }

    public async Task<ItemBrand> FindById(int id)
    {
        var brand = await _dbContext.ItemBrands.FindAsync(id);
        if (brand == null)
        {
            _logger.LogError($"*{GetType().Name}* brand with id: {id} does not exist");
            throw new Exception($"Brand with ID: {id} does not exist");
        }

        return brand;
    }

    public async Task<int?> AddToCatalog(ItemBrand item)
    {
        var newBrand = await _dbContext.ItemBrands.AddAsync(item);
        await _dbContext.SaveChangesAsync();
        return newBrand.Entity.Id;
    }

    public async Task<ItemBrand> UpdateInCatalog(ItemBrand item)
    {
        var newBrand = await FindById(item.Id);
        newBrand.Brand = item.Brand;
        newBrand = _dbContext.ItemBrands.Update(newBrand).Entity;
        await _dbContext.SaveChangesAsync();
        return newBrand;
    }

    public async Task<ItemBrand> RemoveFromCatalog(int id)
    {
        var brand = await FindById(id);
        _dbContext.ItemBrands.Remove(brand);
        await _dbContext.SaveChangesAsync();
        return brand;
    }
}