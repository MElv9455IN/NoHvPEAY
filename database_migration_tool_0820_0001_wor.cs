// 代码生成时间: 2025-08-20 00:01:35
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseMigrationTool
{
    // MigrationTool类用于执行数据库迁移操作
    public static class MigrationTool
    {
        public static async Task MigrateDatabaseAsync<TContext>(
            string connectionString,
            bool forceRecreateDatabase = false,
            bool verbose = false)
            where TContext : DbContext
        {
            // 获取服务提供者
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<TContext>(options =>
                    options.UseSqlServer(connectionString))
                .BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var db = services.GetRequiredService<TContext>();
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger<MigrationTool>();

                try
                {
                    // 检查数据库是否需要重建
                    if (forceRecreateDatabase)
                    {
                        await db.Database.EnsureDeletedAsync();
                        await db.Database.MigrateAsync();
                    }
                    else
                    {
                        // 获取迁移信息
                        var pendingMigrations = db.Database.GetPendingMigrations();

                        if (pendingMigrations.Any())
                        {
                            // 执行迁移
                            await db.Database.MigrateAsync();
                        }
                        else
                        {
                            logger.LogInformation("No pending migrations.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // 错误处理
                    logger.LogError(ex.Message);
                    throw;
                }
            }
        }
    }
}
