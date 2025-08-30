// 代码生成时间: 2025-08-30 19:39:22
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;
using FluentAssertions;
using System.Threading.Tasks;

// 定义一个示例实体类
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
}

// 定义一个示例数据库上下文
public class TestDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
    {
    }
}

// 定义一个集成测试工具类
public class IntegrationTestTool
{
    private readonly TestDbContext _context;

    public IntegrationTestTool(TestDbContext context)
    {
        _context = context;
    }

    // 添加产品的测试方法
    public async Task AddProductAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    // 获取产品的测试方法
    public async Task<Product> GetProductAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }
}

// 定义测试类
public class IntegrationTests
{
    private readonly TestDbContext _context;
    private readonly IntegrationTestTool _testTool;

    public IntegrationTests()
    {
        // 配置内存数据库
        var optionsBuilder = new DbContextOptionsBuilder<TestDbContext>();
        optionsBuilder.UseInMemoryDatabase(databaseName: "TestDatabase");

        _context = new TestDbContext(optionsBuilder.Options);
        _testTool = new IntegrationTestTool(_context);
    }

    [Fact]
    public async Task AddAndGetProduct_ShouldReturnAddedProduct()
    {
        // Arrange
        var product = new Product { Name = "Test Product" };

        // Act
        await _testTool.AddProductAsync(product);
        var addedProduct = await _testTool.GetProductAsync(product.Id);

        // Assert
        addedProduct.Should().NotBeNull();
        addedProduct.Name.Should().BeEquivalentTo(product.Name);
    }

    [Fact]
    public async Task AddAndGetProduct_ShouldHandleMultipleAdds()
    {
        // Arrange
        var product1 = new Product { Name = "Test Product 1" };
        var product2 = new Product { Name = "Test Product 2" };

        // Act
        await _testTool.AddProductAsync(product1);
        await _testTool.AddProductAsync(product2);
        var addedProduct1 = await _testTool.GetProductAsync(product1.Id);
        var addedProduct2 = await _testTool.GetProductAsync(product2.Id);

        // Assert
        addedProduct1.Should().NotBeNull();
        addedProduct1.Name.Should().BeEquivalentTo(product1.Name);
        addedProduct2.Should().NotBeNull();
        addedProduct2.Name.Should().BeEquivalentTo(product2.Name);
    }

    // 确保每个测试后清理数据库
    public async Task DisposeAsync()
    {
        await _context.Database.EnsureDeletedAsync();
    }
}
