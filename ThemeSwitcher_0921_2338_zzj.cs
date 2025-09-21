// 代码生成时间: 2025-09-21 23:38:44
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

// 定义一个实体类来表示主题设置
public class ThemeSetting
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string Theme { get; set; }
    // 可能需要更多的字段来扩展主题设置
}

// 定义一个DbContext来处理数据库操作
public class ApplicationContext : DbContext
{
    public DbSet<ThemeSetting> ThemeSettings { get; set; }
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }
}

// 主题切换服务
public class ThemeSwitcherService
{
    private readonly ApplicationContext _context;

    public ThemeSwitcherService(ApplicationContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    // 异步方法，切换用户主题
    public async Task SwitchThemeAsync(string userId, string theme)
    {
        try
        {
            // 尝试查找现有的主题设置
            var setting = await _context.ThemeSettings.SingleOrDefaultAsync(ts => ts.UserId == userId);
            if (setting == null)
            {
                // 如果没有找到，添加新的设置
                await _context.ThemeSettings.AddAsync(new ThemeSetting { UserId = userId, Theme = theme });
            }
            else
            {
                // 如果找到了，更新主题设置
                setting.Theme = theme;
            }
            // 保存更改到数据库
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            // 适当的错误处理
            Console.WriteLine($"An error occurred while switching theme: {ex.Message}");
            throw;
        }
    }

    // 异步方法，获取用户当前主题
    public async Task<string> GetCurrentThemeAsync(string userId)
    {
        try
        {
            var setting = await _context.ThemeSettings.SingleOrDefaultAsync(ts => ts.UserId == userId);
            // 如果没有设置，返回默认主题
            return setting?.Theme ?? "default";
        }
        catch (Exception ex)
        {
            // 适当的错误处理
            Console.WriteLine($"An error occurred while retrieving theme: {ex.Message}");
            throw;
        }
    }
}
