// 代码生成时间: 2025-08-11 14:42:11
using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using Microsoft.EntityFrameworkCore;

// 定义一个缓存策略类，用于实现缓存逻辑
public class CacheStrategy<TContext> where TContext : DbContext
{
    // 定义缓存对象
    private ObjectCache _cache;
    private const string CACHE_NAME = "EntityFrameworkCacheStrategy";
    private const int CACHE_DURATION = 60; // 缓存持续时间（秒）

    // 构造函数，初始化缓存
    public CacheStrategy()
    {
        _cache = MemoryCache.Default;
    }

    // 获取数据，如果缓存存在则返回缓存，否则执行数据库查询并缓存结果
    public IEnumerable<TEntity> GetDataFromCacheOrDb<TEntity>(Func<TContext, DbSet<TEntity>> getDbSet, string cacheKey, TContext context) where TEntity : class
    {
        try
        {
            // 尝试从缓存中获取数据
            var cachedData = _cache[cacheKey] as List<TEntity>;
            if (cachedData != null)
            {
                return cachedData;
            }

            // 缓存未命中，从数据库获取数据
            var dbSet = getDbSet(context);
            var data = dbSet.ToList();

            // 将查询结果添加到缓存
            _cache.Add(cacheKey, data, DateTimeOffset.Now.AddSeconds(CACHE_DURATION));

            return data;
        }
        catch (Exception ex)
        {
            // 错误处理：记录日志并抛出异常
            Console.WriteLine($"Error occurred: {ex.Message}");
            throw;
        }
    }

    // 清除特定缓存项
    public void ClearCache(string cacheKey)
    {
        _cache.Remove(cacheKey);
    }

    // 清除所有缓存项
    public void ClearAllCache()
    {
        _cache.Dispose();
    }
}
