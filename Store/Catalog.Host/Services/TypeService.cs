using Catalog.Host.DbContextData.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class TypeService: ICatalogService<ItemType>
{
    private readonly ICatalogRepository<ItemType> _typeRepository;
    private readonly ILogger<TypeService> _logger;
    

    public TypeService(ICatalogRepository<ItemType> typeRepository,
        ILogger<TypeService> logger)
    {
        _typeRepository = typeRepository;
        _logger = logger;
    }
    public async Task<List<ItemType>> GetCatalog()
    {
        return await _typeRepository.GetCatalog();
    }

    public async Task<ItemType> FindById(int id)
    {
        return await _typeRepository.FindById(id);
    }

    public async Task<int?> AddToCatalog(ItemType item)
    {
        return await _typeRepository.AddToCatalog(item);
    }

    public async Task<ItemType> UpdateInCatalog(ItemType item)
    {
        return await _typeRepository.UpdateInCatalog(item);
    }

    public async Task<ItemType> RemoveFromCatalog(int id)
    {
        return await _typeRepository.RemoveFromCatalog(id);
    }
}