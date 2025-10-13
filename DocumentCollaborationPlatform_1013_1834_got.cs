// 代码生成时间: 2025-10-13 18:34:51
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// 定义文档协作平台的实体类
public class Document
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public List<UserDocument> UserDocuments { get; set; } = new List<UserDocument>();
}

// 定义用户和文档的关系
public class UserDocument
{
    public int UserId { get; set; }
    public User User { get; set; }
    public int DocumentId { get; set; }
    public Document Document { get; set; }
}

// 定义用户类
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<UserDocument> UserDocuments { get; set; } = new List<UserDocument>();
}

// 创建文档协作平台的数据库上下文
public class CollaborationDbContext : DbContext
{
    public DbSet<Document> Documents { get; set; }
    public DbSet<User> Users { get; set; }

    public CollaborationDbContext(DbContextOptions<CollaborationDbContext> options) : base(options)
    {
    }
}

// 文档协作平台服务类
public class CollaborationService
{
    private readonly CollaborationDbContext _context;

    public CollaborationService(CollaborationDbContext context)
    {
        _context = context;
    }

    // 添加新文档
    public async Task<Document> AddDocumentAsync(string title, string content)
    {
        var document = new Document { Title = title, Content = content };
        _context.Documents.Add(document);
        await _context.SaveChangesAsync();
        return document;
    }

    // 获取所有文档
    public async Task<List<Document>> GetAllDocumentsAsync()
    {
        return await _context.Documents.ToListAsync();
    }

    // 添加用户到文档
    public async Task<UserDocument> AddUserToDocumentAsync(int userId, int documentId)
    {
        var userDocument = new UserDocument { UserId = userId, DocumentId = documentId };
        _context.UserDocuments.Add(userDocument);
        await _context.SaveChangesAsync();
        return userDocument;
    }

    // 获取文档的所有用户
    public async Task<List<User>> GetUsersForDocumentAsync(int documentId)
    {
        return await _context.UserDocuments
            .Where(ud => ud.DocumentId == documentId)
            .Select(ud => ud.User)
            .ToListAsync();
    }
}
