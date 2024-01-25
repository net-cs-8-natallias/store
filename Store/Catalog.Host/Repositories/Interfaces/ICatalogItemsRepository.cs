using Catalog.Host.DbContextData.Entities;
using Catalog.Host.Models;

namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogItemsRepository: ICatalogRepository<CatalogItem>
{
    Task UpdateItemsStock(List<OrderItem> items);
    Task<List<Item>> GetItemsByCatalogItemId(int catalogItemId);
    
    Task<List<CatalogItem>> GetCatalog(CatalogFilter filter);
}