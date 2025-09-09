// 代码生成时间: 2025-09-09 12:07:31
// <copyright file="NetworkConnectionChecker.cs" company="YourCompany">
// Copyright (c) YourCompany. All rights reserved.
// </copyright>

using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace YourNamespace
{
    /// <summary>
    /// Represents a network connection checker that can be used to verify if the application can connect to a remote host.
    /// </summary>
    public class NetworkConnectionChecker
    {
        private readonly string _host;
        private readonly int _port;

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkConnectionChecker"/> class.
        /// </summary>
        /// <param name="host">The hostname or IP address to check the connection with.</param>
        /// <param name="port">The port number to check the connection on.</param>
        public NetworkConnectionChecker(string host, int port)
        {
            _host = host ?? throw new ArgumentNullException(nameof(host));
            _port = port;
        }

        /// <summary>
        /// Asynchronously checks if the network connection to the specified host and port is successful.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the connection was successful.</returns>
        public async Task<bool> CheckConnectionAsync()
        {
            try
            {
                using (var client = new TcpClient())
                {
                    // Specify the timeout for the connection attempt.
                    client.ReceiveTimeout = 3000; // 3 seconds.
                    client.SendTimeout = 3000; // 3 seconds.

                    await client.ConnectAsync(_host, _port);

                    // If no exceptions are thrown, the connection was successful.
                    return true;
                }
            }
            catch (SocketException e)
            {
                // Handle socket-related errors.
                Console.WriteLine($"SocketException: {e.Message}");
            }
            catch (Exception e)
            {
                // Handle other exceptions that might occur.
                Console.WriteLine($"Exception: {e.Message}");
            }

            // If an exception was caught, the connection was not successful.
            return false;
        }
    }
}
