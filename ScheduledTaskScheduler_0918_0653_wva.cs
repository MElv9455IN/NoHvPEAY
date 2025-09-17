// 代码生成时间: 2025-09-18 06:53:00
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace YourNamespace
{
    // 定时任务调度器
    public class ScheduledTaskScheduler : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly ILogger<ScheduledTaskScheduler> _logger;
        private readonly TimeSpan _interval;
        private bool _isRunning;

        public ScheduledTaskScheduler(ILogger<ScheduledTaskScheduler> logger, TimeSpan interval)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _interval = interval;
        }

        // 启动任务
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting scheduled task...");
            _isRunning = true;
            _timer = new Timer(DoWork, null, TimeSpan.Zero, _interval);
            return Task.CompletedTask;
        }

        // 停止任务
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping scheduled task...");
            _timer?.Change(Timeout.Infinite, 0);
            _isRunning = false;
            return Task.CompletedTask;
        }

        // 执行任务的方法
        private void DoWork(object state)
        {
            _logger.LogInformation("Scheduled task is working...");
            // 这里添加实际的任务逻辑
            try
            {
                // 任务逻辑
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred executing the scheduled task.");
            }
        }

        // 确保资源被正确清理
        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
