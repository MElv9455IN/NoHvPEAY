// 代码生成时间: 2025-09-16 08:11:11
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

/// <summary>
/// DatabaseMigrationTool provides functionality to migrate the database using Entity Framework Core.
/// </summary>
public class DatabaseMigrationTool
{
    private readonly DbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="DatabaseMigrationTool"/> class.
    /// </summary>
    /// <param name="context">The database context to use for migration.</param>
    public DatabaseMigrationTool(DbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Migrates the database to the latest version.
    /// </summary>
    /// <returns>A task that represents the asynchronous migration operation.</returns>
    public async Task MigrateDatabaseAsync()
    {
        try
        {
            // Ensure the database is created if it doesn't exist
            await _context.Database.EnsureCreatedAsync();

            // Apply any pending migrations to the database
            await _context.Database.MigrateAsync();

            Console.WriteLine("Database migration completed successfully.");
        }
        catch (Exception ex)
        {
            // Handle any exceptions that occur during migration
            Console.WriteLine($"An error occurred during database migration: {ex.Message}");
            throw;
        }
    }
}

/// <summary>
/// Program class to run the DatabaseMigrationTool.
/// </summary>
class Program
{
    static async Task Main(string[] args)
    {
        // Assuming a DbContext named MyDbContext is configured correctly with connection string, etc.
        var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
        optionsBuilder.UseSqlServer("MyConnectionString");
        var options = optionsBuilder.Options;

        using (var context = new MyDbContext(options))
        {
            var migrationTool = new DatabaseMigrationTool(context);
            await migrationTool.MigrateDatabaseAsync();
        }
    }
}
