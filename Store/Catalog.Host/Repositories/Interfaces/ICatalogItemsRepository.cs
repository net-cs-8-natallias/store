using Catalog.Host.Data.Entities;
using Catalog.Host.Models;

namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogItemsRepository: ICatalogRepository<CatalogItem>
{
    Task<List<CatalogItem>> GetCatalog(CatalogFilter filter);
    Task UpdateStock(List<OrderItem> items);
}