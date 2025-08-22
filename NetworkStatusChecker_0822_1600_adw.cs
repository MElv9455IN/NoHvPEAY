// 代码生成时间: 2025-08-22 16:00:56
using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace NetworkCheckApp
{
    // 网络连接状态检查器类
    public class NetworkStatusChecker
    {
        private readonly string _testUrl;

        // 构造函数，传入用于检查网络的测试URL
        public NetworkStatusChecker(string testUrl)
        {
            _testUrl = testUrl ?? throw new ArgumentNullException(nameof(testUrl));
        }

        // 异步方法，检查网络连接状态
        public async Task<bool> IsInternetConnectedAsync()
        {
            try
            {
                // 使用Ping类检查网络连接
                using (var ping = new Ping())
                {
                    PingReply reply = await ping.SendPingAsync(_testUrl);
                    return reply.Status == IPStatus.Success;
                }
            }
            catch (PingException)
            {
                // 处理Ping异常
                Console.WriteLine("Ping failed. Check your network connection.");
                return false;
            }
            catch (Exception ex)
            {
                // 处理其他异常
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        // 同步方法，检查网络连接状态
        public bool IsInternetConnected()
        {
            try
            {
                using (var ping = new Ping())
                {
                    // 同步发送Ping请求，可能阻塞主线程，不推荐在UI应用中使用
                    PingReply reply = ping.Send(_testUrl);
                    return reply.Status == IPStatus.Success;
                }
            }
            catch (PingException)
            {
                Console.WriteLine("Ping failed. Check your network connection.");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }
    }

    // 程序入口
    class Program
    {
        static async Task Main(string[] args)
        {
            // 创建网络连接状态检查器实例，使用Google作为测试URL
            var checker = new NetworkStatusChecker("www.google.com");

            // 检查网络连接状态
            bool isConnected = await checker.IsInternetConnectedAsync();

            // 输出结果
            Console.WriteLine($"Is Internet Connected: {isConnected}");
        }
    }
}
