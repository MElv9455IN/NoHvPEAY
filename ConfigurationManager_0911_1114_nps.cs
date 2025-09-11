// 代码生成时间: 2025-09-11 11:14:17
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

// 定义一个名为ConfigurationManager的类，用于管理配置文件
public class ConfigurationManager
{
    // DbContextOptions用于指定数据库上下文的配置
    private readonly DbContextOptions<ConfigurationContext> _options;

    public ConfigurationManager(DbContextOptions<ConfigurationContext> options)
    {
        _options = options;
    }

    // 获取指定键的配置值
    public async Task<string> GetConfigValueAsync(string key)
    {
        using (var context = new ConfigurationContext(_options))
        {
            var config = await context.Configurations.FirstOrDefaultAsync(c => c.Key == key);

            if (config == null)
            {
                throw new KeyNotFoundException($"No configuration found with key: {key}");
            }

            return config.Value;
        }
    }

    // 更新指定键的配置值
    public async Task UpdateConfigValueAsync(string key, string value)
    {
        using (var context = new ConfigurationContext(_options))
        {
            var config = await context.Configurations.FirstOrDefaultAsync(c => c.Key == key);

            if (config == null)
            {
                // 如果配置项不存在，则添加新的配置项
                config = new Configuration { Key = key, Value = value };
                await context.Configurations.AddAsync(config);
            }
            else
            {
                // 如果配置项存在，则更新配置项的值
                config.Value = value;
            }

            await context.SaveChangesAsync();
        }
    }
}

// 定义一个名为Configuration的类，用于表示配置项
public class Configuration
{
    public string Key { get; set; }
    public string Value { get; set; }
}

// 定义一个名为ConfigurationContext的DbContext，用于与数据库进行交互
public class ConfigurationContext : DbContext
{
    public ConfigurationContext(DbContextOptions<ConfigurationContext> options) : base(options)
    {
    }

    public DbSet<Configuration> Configurations { get; set; }
}
