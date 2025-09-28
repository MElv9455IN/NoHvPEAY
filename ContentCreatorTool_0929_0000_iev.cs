// 代码生成时间: 2025-09-29 00:00:29
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

// 定义实体类，对应数据库中的表
public class ContentItem
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public DateTime CreatedAt { get; set; }
}

// 定义数据库上下文
public class ContentDbContext : DbContext
{
    public DbSet<ContentItem> ContentItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // 配置数据库连接字符串
        options.UseSqlServer("Server=(localdb)\mssqllocaldb;Database=ContentDb;Trusted_Connection=True;");
    }
}

// 内容创作工具类
public class ContentCreatorTool
{
    private readonly ContentDbContext _context;

    // 构造函数，依赖注入数据库上下文
    public ContentCreatorTool(ContentDbContext context)
    {
        _context = context;
    }

    // 创建内容项
    public ContentItem CreateContent(string title, string body)
    {
        try
        {
            // 创建新的ContentItem实例
            var contentItem = new ContentItem
            {
                Title = title,
                Body = body,
                CreatedAt = DateTime.Now
            };

            // 将新内容项添加到数据库
            _context.ContentItems.Add(contentItem);
            _context.SaveChanges();

            return contentItem;
        }
        catch (Exception ex)
        {
            // 错误处理
            Console.WriteLine($"Error creating content: {ex.Message}");
            throw;
        }
    }

    // 获取所有内容项
    public IQueryable<ContentItem> GetAllContents()
    {
        try
        {
            return _context.ContentItems;
        }
        catch (Exception ex)
        {
            // 错误处理
            Console.WriteLine($"Error retrieving contents: {ex.Message}");
            throw;
        }
    }
}

// 程序入口点
class Program
{
    static void Main(string[] args)
    {
        // 创建数据库上下文实例
        using (var context = new ContentDbContext())
        {
            // 创建内容创作工具实例
            var contentCreator = new ContentCreatorTool(context);

            // 创建内容项
            var newContent = contentCreator.CreateContent("Sample Title", "Sample Body");
            Console.WriteLine($"Created new content item with ID: {newContent.Id}");

            // 获取所有内容项
            var allContents = contentCreator.GetAllContents().ToList();
            foreach (var content in allContents)
            {
                Console.WriteLine($"Content ID: {content.Id}, Title: {content.Title}, CreatedAt: {content.CreatedAt}");
            }
        }
    }
}