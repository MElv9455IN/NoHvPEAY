// 代码生成时间: 2025-09-08 04:38:41
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

// 定义库存项
public class InventoryItem
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Quantity { get; set; }
}

// 定义库存上下文
public class InventoryContext : DbContext
{
    public DbSet<InventoryItem> InventoryItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer("Server=(localdb)\mssqllocaldb;Database=InventoryDB;Trusted_Connection=True;");
    }
}

// 库存管理服务
public class InventoryService
{
    private readonly InventoryContext _context;

    public InventoryService(InventoryContext context)
    {
        _context = context;
    }

    // 添加库存项
    public async Task AddItemAsync(InventoryItem item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));

        _context.InventoryItems.Add(item);
        await _context.SaveChangesAsync();
    }

    // 更新库存项
    public async Task UpdateItemAsync(InventoryItem item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));

        _context.InventoryItems.Update(item);
        await _context.SaveChangesAsync();
    }

    // 删除库存项
    public async Task DeleteItemAsync(int id)
    {
        var item = await _context.InventoryItems.FindAsync(id);
        if (item == null) throw new KeyNotFoundException("Inventory item not found.");

        _context.InventoryItems.Remove(item);
        await _context.SaveChangesAsync();
    }

    // 获取所有库存项
    public async Task<List<InventoryItem>> GetAllItemsAsync()
    {
        return await _context.InventoryItems.ToListAsync();
    }

    // 根据ID获取库存项
    public async Task<InventoryItem?> GetItemByIdAsync(int id)
    {
        return await _context.InventoryItems.FindAsync(id);
    }
}
