// 代码生成时间: 2025-08-20 13:39:01
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

// Define the database context
public class NotificationContext : DbContext
{
    public NotificationContext(DbContextOptions<NotificationContext> options)
        : base(options)
    {
    }

    // DbSet for storing notifications
    public DbSet<Notification> Notifications { get; set; }
}

// Define the Notification entity
public class Notification
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
}

// Define the NotificationService class
public class NotificationService
{
    private readonly NotificationContext _context;

    public NotificationService(NotificationContext context)
    {
        _context = context;
    }

    // Add a new notification
    public async Task AddNotificationAsync(string title, string content)
    {
        try
        {
            var notification = new Notification
            {
                Title = title,
                Content = content,
                CreatedAt = DateTime.Now
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            // Handle any exceptions that occur during the process
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    // Get all notifications
    public async Task<List<Notification>> GetAllNotificationsAsync()
    {
        try
        {
            return await _context.Notifications.ToListAsync();
        }
        catch (Exception ex)
        {
            // Handle any exceptions that occur during the process
            Console.WriteLine($"An error occurred: {ex.Message}");
            return null;
        }
    }
}

// Example usage of the NotificationService
class Program
{
    static async Task Main(string[] args)
    {
        // Setup the database context options
        var optionsBuilder = new DbContextOptionsBuilder<NotificationContext>();
        optionsBuilder.UseSqlServer("YourConnectionStringHere");
        using var context = new NotificationContext(optionsBuilder.Options);

        // Create an instance of the NotificationService
        var notificationService = new NotificationService(context);

        // Add a new notification
        await notificationService.AddNotificationAsync("New Message", "Hello, this is a new message!");

        // Get all notifications
        var notifications = await notificationService.GetAllNotificationsAsync();

        // Print the notifications
        foreach (var notification in notifications)
        {
            Console.WriteLine($"Title: {notification.Title}, Content: {notification.Content}, Created At: {notification.CreatedAt}");
        }
    }
}