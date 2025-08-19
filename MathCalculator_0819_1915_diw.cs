// 代码生成时间: 2025-08-19 19:15:13
using System;

namespace MathTools
{
    /// <summary>
    /// MathCalculator类提供一个简单的数学计算工具集，包括加、减、乘、除等操作。
    /// </summary>
    public class MathCalculator
    {
        /// <summary>
        /// 计算两个数值的和。
        /// </summary>
        /// <param name="a">第一个数值。</param>
        /// <param name="b">第二个数值。</param>
        /// <returns>两个数值的和。</returns>
        public double Add(double a, double b)
        {
            return a + b;
        }

        /// <summary>
        /// 计算两个数值的差。
        /// </summary>
        /// <param name="a">第一个数值。</param>
        /// <param name="b">第二个数值。</param>
        /// <returns>两个数值的差。</returns>
        public double Subtract(double a, double b)
        {
            return a - b;
        }

        /// <summary>
        /// 计算两个数值的乘积。
        /// </summary>
        /// <param name="a">第一个数值。</param>
        /// <param name="b">第二个数值。</param>
        /// <returns>两个数值的乘积。</returns>
        public double Multiply(double a, double b)
        {
            return a * b;
        }

        /// <summary>
        /// 计算两个数值的商。
        /// </summary>
        /// <param name="a">被除数。</param>
        /// <param name="b">除数。</param>
        /// <returns>两个数值的商。</returns>
        /// <exception cref="DivideByZeroException">当除数为0时抛出。</exception>
        public double Divide(double a, double b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero.");
            }
            return a / b;
        }

        /// <summary>
        /// 执行数学计算并根据操作类型返回结果。
        /// </summary>
        /// <param name="operation">操作类型（加、减、乘、除）。</param>
        /// <param name="a">第一个数值。</param>
        /// <param name="b">第二个数值。</param>
        /// <returns>计算结果。</returns>
        public double Calculate(string operation, double a, double b)
        {
            switch (operation)
            {
                case "add":
                    return Add(a, b);
                case "subtract":
                    return Subtract(a, b);
                case "multiply":
                    return Multiply(a, b);
                case "divide":
                    return Divide(a, b);
                default:
                    throw new ArgumentException("Invalid operation.");
            }
        }
    }
}
