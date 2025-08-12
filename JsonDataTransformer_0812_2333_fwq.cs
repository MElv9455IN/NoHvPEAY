// 代码生成时间: 2025-08-12 23:33:59
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

// 命名空间，用于包含以上程序的相关类
namespace JsonDataTransformerApp
{
    // Json数据转换器类
    public class JsonDataTransformer
    {
        // 将JSON字符串转换为对象
        public T ConvertFromJson<T>(string jsonString)
        {
            try
            {
                // 使用System.Text.Json库进行反序列化
                return JsonSerializer.Deserialize<T>(jsonString);
            }
            catch (JsonException ex)
            {
                // 错误处理：如果JSON格式不正确，则抛出异常
                throw new InvalidOperationException("Invalid JSON format", ex);
            }
        }

        // 将对象转换为JSON字符串
        public string ConvertToJson<T>(T data)
        {
            try
            {
                // 使用System.Text.Json库进行序列化
                return JsonSerializer.Serialize(data);
            }
            catch (JsonException ex)
            {
                // 错误处理：如果数据序列化失败，则抛出异常
                throw new InvalidOperationException("Invalid data for JSON serialization", ex);
            }
        }
    }

    // 主类，用于演示JsonDataTransformer的使用
    class Program
    {
        static void Main(string[] args)
        {
            // 创建JsonDataTransformer实例
            var transformer = new JsonDataTransformer();

            // 示例数据对象
            var person = new
            {
                Name = "John Doe",
                Age = 30
            };

            // 将对象转换为JSON字符串
            string jsonString = transformer.ConvertToJson(person);
            Console.WriteLine($"JSON Output: {jsonString}");

            // 将JSON字符串转换回对象
            var personFromJson = transformer.ConvertFromJson<dynamic>(jsonString);
            Console.WriteLine($"Name: {personFromJson.Name}, Age: {personFromJson.Age}");
        }
    }
}
