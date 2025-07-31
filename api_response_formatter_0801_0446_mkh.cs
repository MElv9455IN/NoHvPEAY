// 代码生成时间: 2025-08-01 04:46:48
using System;
using System.Collections.Generic;

// Define a namespace for the API response formatter
namespace ApiResponseFormatter
{
    public class ApiResponseFormatter
    {
        // Method to format a successful API response
        public static Dictionary<string, object> FormatSuccess(string message, dynamic data = null)
        {
            return new Dictionary<string, object>
            {
                { "status", "success" },
                { "message", message },
                { "data", data }
            };
        }

        // Method to format an API response with an error
        public static Dictionary<string, object> FormatError(string message, int? code = null)
        {
            return new Dictionary<string, object>
            {
                { "status", "error" },
                { "message", message },
                { "code", code }
            };
        }
    }
}
