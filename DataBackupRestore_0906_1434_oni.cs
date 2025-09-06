// 代码生成时间: 2025-09-06 14:34:49
using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Migrations;

// Define the DataBackupRestore class
public class DataBackupRestore<TContext> where TContext : DbContext
{
    private readonly TContext _context;

    // Constructor to inject the DbContext
    public DataBackupRestore(TContext context)
    {
        _context = context;
    }

    // Method to backup the database to a file
    public void BackupDatabase(string backupFilePath)
    {
        try
        {
            // Use the DbContext to create a database creator
            var databaseCreator = _context.Database.GetService<IRelationalDatabaseCreator>();

            // Check if the database is already created
            if (databaseCreator.Exists())
            {
                // Get the database name
                var databaseName = _context.Database.GetDbConnection().Database;

                // Backup the database to the specified file path
                _context.Database.EnsureDeleted();
                _context.Database.Migrate();
                _context.Database.EnsureCreated();
                var backupCommand = $"BACKUP DATABASE {databaseName} TO DISK = '{backupFilePath}'";
                _context.Database.ExecuteSqlCommand(backupCommand);
                Console.WriteLine($"Database backed up successfully to {backupFilePath}");
            }
            else
            {
                Console.WriteLine("Database does not exist.");
            }
        }
        catch (Exception ex)
        {
            // Handle any exceptions that occur during backup
            Console.WriteLine($"An error occurred during backup: {ex.Message}");
        }
    }

    // Method to restore the database from a file
    public void RestoreDatabase(string backupFilePath)
    {
        try
        {
            // Use the DbContext to create a database creator
            var databaseCreator = _context.Database.GetService<IRelationalDatabaseCreator>();

            // Check if the database already exists
            if (databaseCreator.Exists())
            {
                _context.Database.EnsureDeleted();
            }

            // Restore the database from the specified file path
            var restoreCommand = $"RESTORE DATABASE {_context.Database.GetDbConnection().Database} FROM DISK = '{backupFilePath}'";
            _context.Database.ExecuteSqlCommand(restoreCommand);
            Console.WriteLine($"Database restored successfully from {backupFilePath}");
        }
        catch (Exception ex)
        {
            // Handle any exceptions that occur during restore
            Console.WriteLine($"An error occurred during restore: {ex.Message}");
        }
    }
}
