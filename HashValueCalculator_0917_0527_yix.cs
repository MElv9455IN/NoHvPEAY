// 代码生成时间: 2025-09-17 05:27:48
using System;
using System.Security.Cryptography;
using System.Text;

namespace HashValueCalculator
{
    /// <summary>
    /// 哈希值计算工具类
    /// </summary>
    public class HashValueCalculator
    {
        /// <summary>
        /// 计算字符串的哈希值
        /// </summary>
        /// <param name="input">要计算哈希值的字符串</param>
        /// <param name="hashAlgorithm">哈希算法类型</param>
        /// <returns>计算得到的哈希值字符串</returns>
        public string CalculateHash(string input, HashAlgorithmType hashAlgorithm)
        {
            try
            {
                switch (hashAlgorithm)
                {
                    case HashAlgorithmType.MD5:
                        return CalculateMd5Hash(input);
                    case HashAlgorithmType.SHA256:
                        return CalculateSha256Hash(input);
                    default:
                        throw new ArgumentException("Unsupported hash algorithm.");
                }
            }
            catch (Exception ex)
            {
                // 错误处理：记录日志，抛出异常等
                Console.WriteLine($"Error calculating hash: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// 计算MD5哈希值
        /// </summary>
        /// <param name="input">要计算哈希值的字符串</param>
        /// <returns>MD5哈希值字符串</returns>
        private string CalculateMd5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes(input);
                var hashBytes = md5.ComputeHash(inputBytes);
                return BitConverter.ToString(hashBytes).Replace("-", string.Empty);
            }
        }

        /// <summary>
        /// 计算SHA256哈希值
        /// </summary>
        /// <param name="input">要计算哈希值的字符串</param>
        /// <returns>SHA256哈希值字符串</returns>
        private string CalculateSha256Hash(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes(input);
                var hashBytes = sha256.ComputeHash(inputBytes);
                return BitConverter.ToString(hashBytes).Replace("-", string.Empty);
            }
        }
    }

    /// <summary>
    /// 哈希算法枚举
    /// </summary>
    public enum HashAlgorithmType
    {
        MD5,
        SHA256
    }
}
