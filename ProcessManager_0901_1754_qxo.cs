// 代码生成时间: 2025-09-01 17:54:05
using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;

// 进程管理器类
public class ProcessManager
{
    // 获取所有进程信息
    public List<ProcessInfo> GetAllProcesses()
    {
        try
        {
            var processes = Process.GetProcesses();
            var processInfos = new List<ProcessInfo>();

            foreach (var process in processes)
            {
                processInfos.Add(new ProcessInfo
                {
                    Id = process.Id,
                    Name = process.ProcessName,
                    StartTime = process.StartTime,
                    TotalProcessorTime = process.TotalProcessorTime
                });
            }

            return processInfos;
        }
        catch (Exception ex)
        {
            // 处理异常
            Console.WriteLine($"Error: {ex.Message}");
            return null;
        }
    }

    // 终止指定进程
    public bool TerminateProcess(int processId)
    {
        try
        {
            var process = Process.GetProcessById(processId);
            if (process != null)
            {
                process.Kill();
                process.WaitForExit();
                return true;
            }
            else
            {
                Console.WriteLine($"Process with ID {processId} not found.");
                return false;
            }
        }
        catch (Exception ex)
        {
            // 处理异常
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }
    }
}

// 进程信息类
public class ProcessInfo
{
    // 进程ID
    public int Id { get; set; }

    // 进程名称
    public string Name { get; set; }

    // 进程启动时间
    public DateTime StartTime { get; set; }

    // 进程总处理器时间
    public TimeSpan TotalProcessorTime { get; set; }
}
