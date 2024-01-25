using Basket.Host.Models;

namespace Basket.Host.Services.Interfaces;

public interface IBasketService
{
    Task AddItem(string userId, Item item);
    Task RemoveItem(string userId, Item item);
    Task<List<Item>> GetItems(string userId);
}