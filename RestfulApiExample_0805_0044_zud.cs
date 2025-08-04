// 代码生成时间: 2025-08-05 00:44:39
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
# 增强安全性
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

// 定义一个示例Model
public class Product
# NOTE: 重要实现细节
{
    public int Id { get; set; }
# 扩展功能模块
    public string Name { get; set; }
    public decimal Price { get; set; }
}

// DbContext扩展Entity Framework Core
public class ApplicationDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
}

// RESTful API控制器
[Route("api/[controller]/[action]"])
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProductsController(ApplicationDbContext context)
    {
# 改进用户体验
        _context = context;
    }

    // 获取所有产品
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
    {
        return await _context.Products.ToListAsync();
    }

    // 通过ID获取单个产品
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProductById(int id)
# NOTE: 重要实现细节
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return product;
    }

    // 添加新产品
    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
    }

    // 更新现有产品
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateProduct(int id, Product product)
    {
        if (id != product.Id)
        {
            return BadRequest();
        }
        _context.Entry(product).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
# TODO: 优化性能
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Products.Any(e => e.Id == id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return NoContent();
    }

    // 删除产品
    [HttpDelete("{id:int}")]
# NOTE: 重要实现细节
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
