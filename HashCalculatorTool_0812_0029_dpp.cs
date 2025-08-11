// 代码生成时间: 2025-08-12 00:29:35
using System;
using System.Security.Cryptography;
# NOTE: 重要实现细节
using System.Text;

// 哈希值计算工具类
public class HashCalculatorTool
{
    // 计算字符串的哈希值
    public static string CalculateHash(string input, string hashAlgorithm)
    {
        try
        {
            // 根据算法名称获取相应的哈希算法实例
            var hashAlgorithmInstance = HashAlgorithm.Create(hashAlgorithm);
            if (hashAlgorithmInstance == null)
            {
                throw new ArgumentException($"Unsupported hash algorithm: {hashAlgorithm}");
            }

            // 将输入字符串转换为字节数组
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            // 计算哈希值
# 添加错误处理
            byte[] hashBytes = hashAlgorithmInstance.ComputeHash(inputBytes);

            // 将字节数组转换为十六进制字符串
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashBytes)
# 增强安全性
            {
                sb.AppendFormat("{0:x2}", b);
            }
            return sb.ToString();
        }
# 改进用户体验
        catch (Exception ex)
        {
# 添加错误处理
            // 错误处理
            Console.WriteLine($"Error calculating hash: {ex.Message}");
            return null;
        }
    }

    // 主函数，用于演示哈希值计算工具的使用
    public static void Main(string[] args)
    {
        // 测试计算字符串的MD5哈希值
        string input = "Hello, World!";
        string md5Hash = CalculateHash(input, "MD5");
        Console.WriteLine($"MD5 Hash of '{input}': {md5Hash}");

        // 测试计算字符串的SHA256哈希值
        string sha256Hash = CalculateHash(input, "SHA256");
        Console.WriteLine($"SHA256 Hash of '{input}': {sha256Hash}");
# FIXME: 处理边界情况
    }
}
