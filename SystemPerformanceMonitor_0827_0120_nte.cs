// 代码生成时间: 2025-08-27 01:20:04
using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;

// 定义一个用于监控系统性能的类
public class SystemPerformanceMonitor
{
    // 定义数据库上下文
    private readonly DbContext _dbContext;

    // 构造函数，注入数据库上下文
    public SystemPerformanceMonitor(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // 获取系统CPU使用率
    public double GetCpuUsage()
    {
        try
        {
            PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            return cpuCounter.NextValue();
        }
        catch (Exception ex)
        {
            // 错误处理
            Console.WriteLine($"Error retrieving CPU usage: {ex.Message}");
            return -1;
        }
    }

    // 获取系统内存使用情况
    public double GetMemoryUsage()
    {
        try
        {
            PerformanceCounter memoryCounter = new PerformanceCounter("Memory", "Available MBytes");
            long availableMemory = memoryCounter.NextValue();
            long totalMemory = new PerformanceCounter("Memory", "Total Visible Memory Size").NextValue();
            return 1 - (availableMemory / (double)totalMemory);
        }
        catch (Exception ex)
        {
            // 错误处理
            Console.WriteLine($"Error retrieving memory usage: {ex.Message}");
            return -1;
        }
    }

    // 获取磁盘使用情况
    public double GetDiskUsage(string driveName)
    {
        try
        {
            DriveInfo drive = new DriveInfo(driveName);
            return (drive.TotalSize - drive.AvailableFreeSpace) / (double)drive.TotalSize;
        }
        catch (Exception ex)
        {
            // 错误处理
            Console.WriteLine($"Error retrieving disk usage for {driveName}: {ex.Message}");
            return -1;
        }
    }

    // 示例方法：打印系统性能信息
    public void PrintSystemPerformanceInfo()
    {
        Console.WriteLine($"CPU Usage: {GetCpuUsage()}%");
        Console.WriteLine($"Memory Usage: {GetMemoryUsage()}%");
        Console.WriteLine($"Disk Usage (C:): {GetDiskUsage("C:")}%");
    }
}
