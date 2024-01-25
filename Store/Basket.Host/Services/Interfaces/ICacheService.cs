using StackExchange.Redis;

namespace Basket.Host.Services.Interfaces;

public interface ICacheService
{
    Task AddOrUpdateAsync(string userId, int itemId, int quantity);
    Task RemoveOrUpdateAsync(string userId, int itemId, int quantity);
    Task<HashEntry[]>GetAsync(string userId);
    
    
}