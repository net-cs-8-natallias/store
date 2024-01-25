using Catalog.Host.DbContextData.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CategoryService: ICatalogService<ItemCategory>
{
    private readonly ICatalogRepository<ItemCategory> _categoryRepository;
    private readonly ILogger<CategoryService> _logger;
    

    public CategoryService(ICatalogRepository<ItemCategory> categoryRepository,
        ILogger<CategoryService> logger)
    {
        _categoryRepository = categoryRepository;
        _logger = logger;
    }
    public async Task<List<ItemCategory>> GetCatalog()
    {
        return await _categoryRepository.GetCatalog();
    }

    public async Task<ItemCategory> FindById(int id)
    {
        return await _categoryRepository.FindById(id);
    }

    public async Task<int?> AddToCatalog(ItemCategory item)
    {
        return await _categoryRepository.AddToCatalog(item);
    }

    public async Task<ItemCategory> UpdateInCatalog(ItemCategory item)
    {
        return await _categoryRepository.UpdateInCatalog(item);
    }

    public async Task<ItemCategory> RemoveFromCatalog(int id)
    {
        return await _categoryRepository.RemoveFromCatalog(id);
    }
}