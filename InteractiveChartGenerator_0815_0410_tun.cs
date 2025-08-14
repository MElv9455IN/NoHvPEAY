// 代码生成时间: 2025-08-15 04:10:36
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Data.SqlClient;

// 定义图表数据模型
public class ChartData
{
    public int Id { get; set; }
    public string Category { get; set; }
    public double Value { get; set; }
}

// 定义图表数据上下文
public class ChartContext : DbContext
{
    public DbSet<ChartData> ChartDatas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\mssqllocaldb;Database=ChartDB;Trusted_Connection=True;");
    }
}

// 定义交互式图表生成器服务
public class InteractiveChartGenerator
{
    private readonly ChartContext _context;

    public InteractiveChartGenerator(ChartContext context)
    {
        _context = context;
    }

    // 生成图表数据
    public async Task<List<ChartData>> GenerateChartDataAsync(string category, double minValue, double maxValue)
    {
        try
        {
            var random = new Random();
            var chartDataList = new List<ChartData>();

            for (int i = 0; i < 10; i++)
            {
                var data = new ChartData
                {
                    Category = category,
                    Value = random.NextDouble() * (maxValue - minValue) + minValue
                };
                chartDataList.Add(data);
            }

            await _context.ChartDatas.AddRangeAsync(chartDataList);
            await _context.SaveChangesAsync();

            return chartDataList;
        }
        catch (Exception ex)
        {
            // 处理错误
            Console.WriteLine($"Error generating chart data: {ex.Message}");
            throw;
        }
    }
}

// 定义程序入口类
class Program
{
    static async Task Main(string[] args)
    {
        // 创建图表数据上下文
        var context = new ChartContext();
        await context.Database.EnsureCreatedAsync();

        // 创建交互式图表生成器
        var generator = new InteractiveChartGenerator(context);

        // 生成图表数据
        var chartData = await generator.GenerateChartDataAsync("Sales", 100, 1000);

        // 打印图表数据
        foreach (var data in chartData)
        {
            Console.WriteLine($"Category: {data.Category}, Value: {data.Value}");
        }
    }
}