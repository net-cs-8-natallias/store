using Catalog.Host.DbContextData.Entities;
using Catalog.Host.Models;

namespace Catalog.Host.Repositories.Interfaces;

public interface IItemRepository: ICatalogRepository<Item>
{
    Task UpdateItemsStock(List<OrderItem> items);
    Task<List<Item>> GetItemsByCatalogItemId(int catalogItemId);
}