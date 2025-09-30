// 代码生成时间: 2025-09-30 20:41:50
using System;
using System.Net;
using System.Collections.Concurrent;
using System.Threading.Tasks;

// 定义一个Dns解析和缓存工具类
public class DnsResolverCache
{
    // 缓存DNS解析结果的并发字典
    private readonly ConcurrentDictionary<string, (IPAddress[] Addresses, DateTime ExpiryTime)> _cache = new ConcurrentDictionary<string, (IPAddress[], DateTime)>();
    private readonly TimeSpan _cacheLifeSpan = TimeSpan.FromMinutes(5); // 设置缓存有效期为5分钟

    // 异步解析DNS并缓存结果
    public async Task<IPAddress[]> ResolveDnsAsync(string host)
    {
        IPAddress[] addresses;
        // 尝试从缓存中获取解析结果
        if (_cache.TryGetValue(host, out var cachedResult))
        {
            // 检查缓存结果是否过期
            if (DateTime.UtcNow < cachedResult.ExpiryTime)
            {
                return cachedResult.Addresses;
            }
        }

        // 缓存中没有或已过期，执行DNS解析
        addresses = await Dns.GetHostAddressesAsync(host);

        // 将解析结果缓存起来
        _cache[host] = (addresses, DateTime.UtcNow.Add(_cacheLifeSpan));

        return addresses;
    }

    // 同步解析DNS（如果需要）
    public IPAddress[] ResolveDns(string host)
    {
        IPAddress[] addresses;
        // 尝试从缓存中获取解析结果
        if (_cache.TryGetValue(host, out var cachedResult))
        {
            // 检查缓存结果是否过期
            if (DateTime.UtcNow < cachedResult.ExpiryTime)
            {
                return cachedResult.Addresses;
            }
        }

        // 缓存中没有或已过期，执行DNS解析
        try
        {
            addresses = Dns.GetHostAddresses(host);
        }
        catch (Exception ex)
        {
            // 处理DNS解析错误
            throw new InvalidOperationException($"Failed to resolve DNS for host {host}. Error: {ex.Message}", ex);
        }

        // 将解析结果缓存起来
        _cache[host] = (addresses, DateTime.UtcNow.Add(_cacheLifeSpan));

        return addresses;
    }

    // 清除特定主机的缓存
    public void ClearCache(string host)
    {
        _cache.TryRemove(host, out _);
    }

    // 清除所有缓存
    public void ClearAllCache()
    {
        _cache.Clear();
    }
}
