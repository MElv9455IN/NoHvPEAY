// 代码生成时间: 2025-09-07 15:55:14
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace LogAnalysisTool
{
    public class LogFile
    {
        public string Content { get; set; }
    }

    public class LogContext : DbContext
    {
        public DbSet<LogFile> LogFiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=(localdb)\mssqllocaldb;Database=LogDatabase;Trusted_Connection=True;");
        }
    }

    public class LogParser
    {
        private readonly LogContext _context;

        public LogParser(LogContext context)
        {
            _context = context;
        }

        // 解析日志文件并存储到数据库中
        public void ParseAndStoreLogs(string logFilePath)
        {
            if (string.IsNullOrEmpty(logFilePath))
                throw new ArgumentException("Log file path cannot be empty.");

            try
            {
                string[] lines = File.ReadAllLines(logFilePath);
                foreach (string line in lines)
                {
                    // 假设日志文件的格式为：[2023-04-01 12:34:56] INFO Some message
                    var logEntry = ParseLogLine(line);
                    if (logEntry != null)
                    {
                        _context.LogFiles.Add(logEntry);
                    }
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // 错误处理
                Console.WriteLine($"Error occurred while parsing logs: {ex.Message}");
            }
        }

        // 解析单行日志
        private LogFile ParseLogLine(string logLine)
        {
            // 定义正则表达式以匹配日志行
            var regex = new Regex("^\[(?<date>.+)\] (?<level>INFO|ERROR|WARNING|DEBUG) (?<message>.+)");
d
            Match match = regex.Match(logLine);

            if (!match.Success)
                return null;

            var logEntry = new LogFile
            {
                Content = $"Date: {match.Groups["date"].Value}, Level: {match.Groups["level"].Value}, Message: {match.Groups["message"].Value}"
            };

            return logEntry;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var context = new LogContext();
            var parser = new LogParser(context);

            // 假设有一个日志文件路径
            string logFilePath = @"C:\Logs\example.log";
            parser.ParseAndStoreLogs(logFilePath);
        }
    }
}
