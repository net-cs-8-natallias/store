using Catalog.Host.Data.Entities;
using Catalog.Host.Models;

namespace Catalog.Host.Services.Interfaces;

public interface IBffService
{
    Task<List<CatalogItem>> GetItems(CatalogFilter filters);
    Task UpdateStock(List<OrderItem> items);
    Task<List<CatalogItem>> GetItems();
    Task<List<ItemBrand>> GetBrands();
    Task<List<ItemType>> GetTypes();
    Task<List<ItemCategory>> GetCategories();
    Task<CatalogItem> GetItem(int id);
    Task<ItemBrand> GetBrand(int id);
    Task<ItemType> GetType(int id);
    Task<ItemCategory> GetCategory(int id);
}