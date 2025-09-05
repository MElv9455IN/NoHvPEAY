// 代码生成时间: 2025-09-06 04:22:46
using System;
using System.Security.Cryptography;
using System.Text;

namespace HashCalculatorApp
{
    /// <summary>
    /// A utility class to compute hash values for given strings.
    /// </summary>
    public class HashCalculator
    {
        /// <summary>
        /// Computes the SHA256 hash of a given string.
# 优化算法效率
        /// </summary>
        /// <param name="input">The string to compute the hash for.</param>
# NOTE: 重要实现细节
        /// <returns>The SHA256 hash as a hexadecimal string.</returns>
# 扩展功能模块
        public string ComputeSHA256Hash(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("Input string must not be null or empty.", nameof(input));
            }

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hash = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", string.Empty).ToLowerInvariant();
            }
        }

        /// <summary>
        /// Computes the SHA512 hash of a given string.
# NOTE: 重要实现细节
        /// </summary>
        /// <param name="input">The string to compute the hash for.</param>
        /// <returns>The SHA512 hash as a hexadecimal string.</returns>
# 改进用户体验
        public string ComputeSHA512Hash(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("Input string must not be null or empty.", nameof(input));
            }
# 优化算法效率

            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hash = sha512.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", string.Empty).ToLowerInvariant();
# 增强安全性
            }
# TODO: 优化性能
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                HashCalculator calculator = new HashCalculator();
                string input = "Hello, World!";
                string sha256Hash = calculator.ComputeSHA256Hash(input);
                string sha512Hash = calculator.ComputeSHA512Hash(input);

                Console.WriteLine($"SHA256 Hash: {sha256Hash}");
                Console.WriteLine($"SHA512 Hash: {sha512Hash}");
            }
            catch (Exception ex)
            {
# NOTE: 重要实现细节
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
# 改进用户体验
