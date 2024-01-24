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
    private readonly IJsonSerializer _jsonSerializer;
        
    public CacheService(IRedisCacheConnectionService redisCacheConnectionService, 
        IOptions<RedisConfig> config, ILogger<ICacheService> logger,
        IJsonSerializer jsonSerializer)
    {
        _redisCacheConnectionService = redisCacheConnectionService;
        _config = config;
        _logger = logger;
        _jsonSerializer = jsonSerializer;
    }
    public async Task AddOrUpdateAsync(string userId, int itemId, int quantity)
    {
        var redis = GetRedisDatabase();
        //HashEntry[] storedValues = await redis.HashGetAllAsync(userId);
        // if (storedValues.Length == 0)
        // {
        //     redis.HashSet(userId, new HashEntry[] { new HashEntry(itemId, 1) });
        // } else 
        if (redis.HashExists(userId, itemId))
        {
            await redis.HashIncrementAsync(userId, itemId, quantity);
        }
        else
        {
            await redis.HashSetAsync(userId, itemId, 1);
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

    private IDatabase GetRedisDatabase() => _redisCacheConnectionService.Connection.GetDatabase();
}