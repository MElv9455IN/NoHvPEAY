// 代码生成时间: 2025-08-25 04:26:37
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

// 定义消息实体
public class Message
{
    public int MessageId { get; set; }
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }
    public int RecipientId { get; set; }
    public virtual User Recipient { get; set; }
}

// 定义用户实体
public class User
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public List<Message> ReceivedMessages { get; set; } = new List<Message>();
}

// 定义数据库上下文
public class NotificationContext : DbContext
{
    public DbSet<Message> Messages { get; set; }
    public DbSet<User> Users { get; set; }

    public NotificationContext(DbContextOptions<NotificationContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Message>()
            .HasOne(m => m.Recipient)
            .WithMany(u => u.ReceivedMessages)
            .HasForeignKey(m => m.RecipientId);
    }
}

// 消息通知系统服务
public class MessageNotificationService
{
    private readonly NotificationContext _context;

    public MessageNotificationService(NotificationContext context)
    {
        _context = context;
    }

    // 发送消息方法
    public bool SendMessage(int recipientId, string messageContent)
    {
        try
        {
            var message = new Message
            {
                Content = messageContent,
                Timestamp = DateTime.Now,
                RecipientId = recipientId
            };

            _context.Messages.Add(message);
            _context.SaveChanges();

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending message: {ex.Message}");
            return false;
        }
    }

    // 获取用户接收到的所有消息
    public IEnumerable<Message> GetMessagesForUser(int userId)
    {
        return _context.Users
            .Where(u => u.UserId == userId)
            .SelectMany(u => u.ReceivedMessages)
            .ToList();
    }
}

// 程序入口点
class Program
{
    static void Main(string[] args)
    {
        var options = new DbContextOptionsBuilder<NotificationContext>()
            .UseSqlServer("Your_Connection_String")
            .Options;

        using (var context = new NotificationContext(options))
        {
            var service = new MessageNotificationService(context);

            // 演示：发送消息
            bool success = service.SendMessage(1, "Hello from Notification System!");
            if (success)
                Console.WriteLine("Message sent successfully.");
            else
                Console.WriteLine("Failed to send message.");

            // 演示：获取消息
            var messages = service.GetMessagesForUser(1);
            foreach (var message in messages)
                Console.WriteLine($"Message: {message.Content}, Timestamp: {message.Timestamp}");
        }
    }
}