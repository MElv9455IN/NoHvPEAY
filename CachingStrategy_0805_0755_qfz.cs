// 代码生成时间: 2025-08-05 07:55:05
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

// 定义缓存键前缀
public class CacheKeyPrefix
{
    public const string Products = "Products_";
}

// 缓存策略接口
public interface ICachingStrategy<T>
{
    T Get(string key);
    void Set(string key, T value);
}

// 实现内存缓存策略
public class MemoryCachingStrategy<T> : ICachingStrategy<T>
{
    private readonly IMemoryCache _memoryCache;
    private readonly ILogger<MemoryCachingStrategy<T>> _logger;

    public MemoryCachingStrategy(IMemoryCache memoryCache, ILogger<MemoryCachingStrategy<T>> logger)
    {
        _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public T Get(string key)
    {
        if (_memoryCache.TryGetValue(key, out T value))
        {
            _logger.LogInformation($"Cache hit for key: {key}");
            return value;
        }
        else
        {
            _logger.LogInformation($"Cache miss for key: {key}");
            return default;
        }
    }

    public void Set(string key, T value)
    {
        _memoryCache.Set(key, value);
        _logger.LogInformation($"Value set in cache for key: {key}");
    }
}

// DbContext 扩展方法，添加缓存策略
public static class DbContextExtensions
{
    public static void AddCachingStrategy<TEntity>(this IServiceCollection services)
        where TEntity : class
    {
        services.AddScoped<ICachingStrategy<TEntity>, MemoryCachingStrategy<TEntity>>();
    }
}

// 示例用法
// services.AddCachingStrategy<Product>();