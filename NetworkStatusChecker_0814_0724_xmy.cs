// 代码生成时间: 2025-08-14 07:24:45
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace NetworkStatusChecker
{
    /// <summary>
    /// A class used to check the network connectivity status.
    /// </summary>
    public class NetworkStatusChecker
    {
        /// <summary>
        /// Checks the network connectivity status by pinging a predefined URL.
        /// </summary>
        /// <param name="url">The URL to ping for network connectivity.</param>
        /// <returns>A boolean indicating whether the network is reachable or not.</returns>
        public async Task<bool> IsNetworkConnectedAsync(string url = "http://www.google.com")
        {
            try
            {
                using (var client = new TcpClient())
                {
                    var task = client.ConnectAsync(url, 80); // Default HTTP port.
                    await Task.WhenAny(task, Task.Delay(3000)); // Wait for either the connection or a timeout.
                    return task.IsCompleted;
                }
            }
            catch (SocketException)
            {
                // Handle socket exceptions, which might indicate a network issue.
                Console.WriteLine("SocketException: Unable to reach the network.");
                return false;
            }
            catch (Exception ex)
            {
                // Handle any other exceptions that might occur.
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            var networkChecker = new NetworkStatusChecker();
            var isConnected = await networkChecker.IsNetworkConnectedAsync();

            if (isConnected)
            {
                Console.WriteLine("Network is connected.");
            }
            else
            {
                Console.WriteLine("Network is not connected.");
            }
        }
    }
}