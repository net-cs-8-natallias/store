using Basket.Host.Configurations;
using Basket.Host.Models;
using Basket.Host.Services.Interfaces;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Basket.Host.Services;

public class CacheService: ICacheService
{
    private readonly IRedisCacheConnectionService _redisCacheConnectionService;
    private readonly IOptions<RedisConfig> _config;
    private readonly ILogger<ICacheService> _logger;
        
    public CacheService(IRedisCacheConnectionService redisCacheConnectionService, 
        IOptions<RedisConfig> config, ILogger<ICacheService> logger)
    {
        _redisCacheConnectionService = redisCacheConnectionService;
        _config = config;
        _logger = logger;
    }
    public async Task AddOrUpdateAsync(string userId, int itemId, int quantity)
    {
        var redis = GetRedisDatabase();
        if (redis.HashExists(userId, itemId))
        {
            await redis.HashIncrementAsync(userId, itemId, quantity);
        }
        else
        {
            await redis.HashSetAsync(userId, itemId, quantity);
        }

        
    }

    public async Task RemoveOrUpdateAsync(string userId, int itemId, int quantity)
    {
        var redis = GetRedisDatabase();
        if (await redis.HashGetAsync(userId, itemId) == quantity)
        {
            await redis.HashDeleteAsync(userId, itemId);
        }
        else
        {
            await redis.HashDecrementAsync(userId, itemId, quantity);
        }
    }

    public async Task<HashEntry[]> GetAsync(string userId)
    {
        var redis = GetRedisDatabase();

        return await redis.HashGetAllAsync(userId);
    }

    public async Task RemoveAllAsync(string userId)
    {
        var redis = GetRedisDatabase();
        await redis.KeyDeleteAsync(userId);
    }

    private IDatabase GetRedisDatabase() => _redisCacheConnectionService.Connection.GetDatabase();
}