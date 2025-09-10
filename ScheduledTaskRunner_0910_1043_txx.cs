// 代码生成时间: 2025-09-10 10:43:38
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;
using Quartz.Impl;

namespace ScheduledTaskRunner
{
    /// <summary>
    /// 定时任务调度器
    /// </summary>
    public class ScheduledTaskRunner : IHostedService, IDisposable
    {
        private readonly IScheduler _scheduler;
        private readonly IServiceProvider _serviceProvider;
        private readonly IDisposable _serviceScope;
        private Task _executingTask;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serviceProvider">服务提供器</param>
        public ScheduledTaskRunner(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _serviceScope = serviceProvider.CreateScope();
            var schedulerFactory = _serviceScope.ServiceProvider.GetRequiredService<ISchedulerFactory>();
            _scheduler = await schedulerFactory.GetScheduler();
        }

        /// <summary>
        /// 启动定时任务
        /// </summary>
        /// <param name="cancellationToken">取消令牌</param>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _scheduler.Start();
            // 在这里添加任务调度配置
            var job = JobBuilder.Create<YourTask>()
                .WithIdentity("YourTask", "group1")
                .Build();
            var trigger = TriggerBuilder.Create()
                .WithCronSchedule("0/30 * * * * ?") // 每30秒执行一次
                .Build();
            await _scheduler.ScheduleJob(job, trigger);
        }

        /// <summary>
        /// 停止定时任务
        /// </summary>
        /// <param name="cancellationToken">取消令牌</param>
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _scheduler.Shutdown();
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            _serviceScope.Dispose();
        }
    }

    /// <summary>
    /// 待执行的任务类
    /// </summary>
    public class YourTask : IJob
    {
        /// <summary>
        /// 任务执行方法
        /// </summary>
        /// <param name="context">任务上下文</param>
        public Task Execute(IJobExecutionContext context)
        {
            // 在这里实现任务逻辑
            Console.WriteLine("Task executed at: " + DateTime.Now);
            return Task.CompletedTask;
        }
    }
}
