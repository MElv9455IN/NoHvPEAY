// 代码生成时间: 2025-08-27 12:43:43
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

// 定义购物车项目实体
public class CartItem
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

// 定义购物车服务
public class ShoppingCartService
{
    private readonly DbContext _context;

    public ShoppingCartService(DbContext context)
    {
        _context = context;
    }

    // 添加项目到购物车
    public async Task AddItemToCartAsync(int productId, int quantity)
    {
        try
        {
            var product = await _context.Set<Product>()
                .FirstOrDefaultAsync(p => p.Id == productId);
            if (product == null)
            {
                throw new InvalidOperationException($"Product with ID {productId} does not exist.");
            }

            var cartItem = await _context.Set<CartItem>()
                .FirstOrDefaultAsync(ci => ci.ProductId == productId);

            if (cartItem == null)
            {
                _context.Set<CartItem>().Add(new CartItem
                {
                    ProductId = productId,
                    ProductName = product.Name,
                    Quantity = quantity,
                    Price = product.Price
                });
            }
            else
            {
                cartItem.Quantity += quantity;
            }

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            // 错误处理逻辑
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    // 从购物车移除项目
    public async Task RemoveItemFromCartAsync(int productId)
    {
        try
        {
            var cartItem = await _context.Set<CartItem>()
                .FirstOrDefaultAsync(ci => ci.ProductId == productId);
            if (cartItem == null)
            {
                throw new InvalidOperationException($"Product with ID {productId} is not in the cart.");
            }

            _context.Set<CartItem>().Remove(cartItem);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            // 错误处理逻辑
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    // 获取购物车项目列表
    public async Task<List<CartItem>> GetCartItemsAsync()
    {
        try
        {
            return await _context.Set<CartItem>().ToListAsync();
        }
        catch (Exception ex)
        {
            // 错误处理逻辑
            Console.WriteLine($"An error occurred: {ex.Message}");
            return new List<CartItem>();
        }
    }
}

// 定义产品实体
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

// 定义数据库上下文
public class ShoppingCartContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<CartItem> CartItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // 配置数据库连接字符串
        optionsBuilder.UseSqlServer("your_connection_string_here");
    }
}

// 程序入口点
class Program
{
    static async Task Main(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ShoppingCartContext>();
        optionsBuilder.UseSqlServer("your_connection_string_here");

        using (var context = new ShoppingCartContext(optionsBuilder.Options))
        {
            var cartService = new ShoppingCartService(context);

            // 添加项目到购物车
            await cartService.AddItemToCartAsync(1, 2);

            // 获取购物车项目列表
            var cartItems = await cartService.GetCartItemsAsync();
            foreach (var item in cartItems)
            {
                Console.WriteLine($"Product: {item.ProductName}, Quantity: {item.Quantity}, Price: {item.Price}");
            }

            // 从购物车移除项目
            await cartService.RemoveItemFromCartAsync(1);
        }
    }
}