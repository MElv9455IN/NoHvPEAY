// 代码生成时间: 2025-09-16 02:41:42
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

// 测试数据生成器类
public class TestDataGenerator<TContext> where TContext : DbContext
{
    private readonly TContext _context;

    // 构造函数
    public TestDataGenerator(TContext context)
    {
        _context = context;
    }

    // 生成测试数据
    public async Task GenerateAsync<T>(int count) where T : class
    {
        try
        {
            // 检查数据库上下文是否已经包含数据
            if (_context.Database.EnsureCreated())
            {
                Console.WriteLine("Database created.");
            }
            else
            {
                Console.WriteLine("Database already exists.");
            }

            // 移除所有现有的数据
            await _context.Database.ExecuteSqlCommandAsync("TRUNCATE TABLE " + typeof(T).Name);

            // 生成指定数量的测试数据
            var testData = GenerateTestData<T>(count);
            await _context.Set<T>().AddRangeAsync(testData);

            // 保存到数据库
            await _context.SaveChangesAsync();

            Console.WriteLine($"Generated {count} {typeof(T).Name} records.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error generating test data: {ex.Message}");
        }
    }

    // 测试数据生成方法
    private IEnumerable<T> GenerateTestData<T>(int count) where T : class
    {
        // 此处实现具体的测试数据生成逻辑
        // 以下为示例代码，根据具体需求实现
        return Enumerable.Range(1, count)
            .Select(i => (T)Activator.CreateInstance(typeof(T), new object[] { }));
    }
}

// 使用示例
// 假设有一个名为AppDbContext的DbContext，并且有一个名为User的实体类
// public class AppDbContext : DbContext
// {
//     public DbSet<User> Users { get; set; }
// }

// public class User
// {
//     public int Id { get; set; }
//     public string Name { get; set; }
// }

// 测试数据生成器的使用方式：
// var context = new AppDbContext();
// var generator = new TestDataGenerator<AppDbContext>(context);
// await generator.GenerateAsync<User>(100);
