// 代码生成时间: 2025-08-21 05:46:48
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ValidationSample
{
    // 表单数据验证器类
    public class FormValidator
    {
        // 验证对象是否有效
        public bool Validate(object entity)
        {
            // 使用DataAnnotations验证数据
            var context = new ValidationContext(entity, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            // 检查是否有验证错误
            bool isValid = Validator.TryValidateObject(entity, context, results, true);
            if (!isValid)
            {
                // 如果有错误，抛出异常
                throw new ValidationException("errors")
                {
                    // 将验证结果添加到异常消息中
                    Data = results.ToDictionary(r => r.MemberNames.FirstOrDefault(), r => r.ErrorMessage)
                };
            }

            return isValid;
        }
    }

    // 验证异常类
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message)
        {
        }

        public IDictionary<string, string> Data { get; set; }
    }
}
