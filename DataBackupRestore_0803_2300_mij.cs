// 代码生成时间: 2025-08-03 23:00:44
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading.Tasks;

/// <summary>
/// A class responsible for data backup and restore operations.
/// </summary>
public class DataBackupRestore
{
    private readonly string _databaseLocation;
    private readonly DbContextOptions<YourDbContext> _options;

    /// <summary>
    /// Initializes a new instance of the DataBackupRestore class.
    /// </summary>
    /// <param name="databaseLocation">The location of the database file.</param>
    /// <param name="options">Database context options.</param>
    public DataBackupRestore(string databaseLocation, DbContextOptions<YourDbContext> options)
    {
        _databaseLocation = databaseLocation ?? throw new ArgumentNullException(nameof(databaseLocation));
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    /// <summary>
    /// Creates a backup of the database.
    /// </summary>
    /// <param name="backupName">The name of the backup file.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task BackupAsync(string backupName)
    {
        try
        {
            // Use the database context to execute the backup command
            await using var context = new YourDbContext(_options);
            var backupPath = Path.Combine(Directory.GetCurrentDirectory(), backupName);
            await context.Database.OpenConnectionAsync();
            await ExecuteSqlBackupAsync(context, backupPath);
            Console.WriteLine($"Backup created successfully at {backupPath}");
        }
        catch (Exception ex)
        {
            // Log the exception and rethrow for error handling in the calling code
            Console.WriteLine($"Error during backup: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Restores the database from a backup.
    /// </summary>
    /// <param name="backupPath">The path to the backup file.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task RestoreAsync(string backupPath)
    {
        try
        {
            // Use the database context to execute the restore command
            await using var context = new YourDbContext(_options);
            await context.Database.OpenConnectionAsync();
            await ExecuteSqlRestoreAsync(context, backupPath);
            Console.WriteLine($"Restore completed successfully from {backupPath}");
        }
        catch (Exception ex)
        {
            // Log the exception and rethrow for error handling in the calling code
            Console.WriteLine($"Error during restore: {ex.Message}");
            throw;
        }
    }

    private async Task ExecuteSqlBackupAsync(DbContext context, string backupPath)
    {
        var sqlCommand = $"BACKUP DATABASE {_databaseLocation} TO DISK = '{backupPath}'";
        await context.Database.ExecuteSqlCommandAsync(sqlCommand);
    }

    private async Task ExecuteSqlRestoreAsync(DbContext context, string backupPath)
    {
        var sqlCommand = $"RESTORE DATABASE {_databaseLocation} FROM DISK = '{backupPath}'";
        await context.Database.ExecuteSqlCommandAsync(sqlCommand);
    }
}

/// <summary>
/// Your entity framework's database context class.
/// </summary>
public class YourDbContext : DbContext
{
    public YourDbContext(DbContextOptions options) : base(options)
    {
    }

    // Define your entity sets and configuration here.
}