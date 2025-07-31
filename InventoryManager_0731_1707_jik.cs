// 代码生成时间: 2025-07-31 17:07:24
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

// 定义库存项模型
public class InventoryItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

// 定义库存系统上下文
public class InventoryContext : DbContext
{
    public DbSet<InventoryItem> InventoryItems { get; set; }

    public InventoryContext(DbContextOptions<InventoryContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<InventoryItem>().ToTable("InventoryItems");
    }
}

// 库存管理系统
public class InventoryManager
{
    private readonly InventoryContext _context;

    public InventoryManager(InventoryContext context)
    {
        _context = context;
    }

    // 添加库存项
    public async Task AddItemAsync(InventoryItem item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));
        if (string.IsNullOrEmpty(item.Name)) throw new ArgumentException("Item name cannot be empty.");

        await _context.InventoryItems.AddAsync(item);
        await _context.SaveChangesAsync();
    }

    // 更新库存项
    public async Task UpdateItemAsync(InventoryItem item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));
        if (string.IsNullOrEmpty(item.Name)) throw new ArgumentException("Item name cannot be empty.");

        _context.InventoryItems.Update(item);
        await _context.SaveChangesAsync();
    }

    // 删除库存项
    public async Task DeleteItemAsync(int id)
    {
        var item = await _context.InventoryItems.FindAsync(id);
        if (item == null) throw new InvalidOperationException("Item not found.");

        _context.InventoryItems.Remove(item);
        await _context.SaveChangesAsync();
    }

    // 获取库存项
    public async Task<List<InventoryItem>> GetAllItemsAsync()
    {
        return await _context.InventoryItems.ToListAsync();
    }

    // 根据ID获取库存项
    public async Task<InventoryItem> GetItemByIdAsync(int id)
    {
        return await _context.InventoryItems.FindAsync(id);
    }
}
