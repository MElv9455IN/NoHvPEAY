// 代码生成时间: 2025-08-12 07:50:59
using System;
using System.IO;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

// 定义一个数据备份和恢复的类
public class DataBackupRestore
{
    private readonly string _connectionString;

    // 构造函数
    public DataBackupRestore(string connectionString)
    {
        _connectionString = connectionString;
    }

    // 数据备份方法
    public string Backup(string backupFilePath)
    {
        try
        {
            using (var context = new YourDbContext(_connectionString)) // 使用你的DbContext实例
            {
                // 获取数据库服务器上的文件名称
                var serverFilePath = context.Database.SqlQuery<string>("SELECT DB_NAME() AS DatabaseName").Single();

                // 执行备份命令
                var command = context.Database.Connection.CreateCommand();
                command.CommandText = $"BACKUP DATABASE {serverFilePath} TO DISK = '{backupFilePath}'";
                command.ExecuteNonQuery();
                return $"Database backup was successful. File saved to {backupFilePath}";
            }
        }
        catch (Exception ex)
        {
            // 错误处理
            return $"An error occurred while backing up the database: {ex.Message}";
        }
    }

    // 数据恢复方法
    public string Restore(string backupFilePath)
    {
        try
        {
            using (var context = new YourDbContext(_connectionString)) // 使用你的DbContext实例
            {
                // 执行恢复命令
                var command = context.Database.Connection.CreateCommand();
                command.CommandText = $"RESTORE DATABASE {context.Database.SqlQuery<string>("SELECT DB_NAME() AS DatabaseName").Single()} FROM DISK = '{backupFilePath}'";
                command.ExecuteNonQuery();
                return $"Database restore was successful from file {backupFilePath}";
            }
        }
        catch (Exception ex)
        {
            // 错误处理
            return $"An error occurred while restoring the database: {ex.Message}";
        }
    }
}

// 定义一个继承自DbContext的类，用于操作数据库
public class YourDbContext : DbContext
{
    public YourDbContext(string connectionString) : base(connectionString)
    {
    }

    // 定义你的数据库集合和配置
    // public DbSet<YourEntity> YourEntities { get; set; }
}

// 使用示例
// var backupRestore = new DataBackupRestore("your_connection_string_here");
// var backupResult = backupRestore.Backup("path_to_your_backup_file.bak");
// Console.WriteLine(backupResult);
// var restoreResult = backupRestore.Restore("path_to_your_backup_file.bak");
// Console.WriteLine(restoreResult);
