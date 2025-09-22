// 代码生成时间: 2025-09-23 06:44:56
using System;

namespace MathematicalCalculations
{
    /// <summary>
    /// Mathematical calculations service providing basic operations.
    /// </summary>
    public class MathematicalCalculationsService
    {
        /// <summary>
        /// Adds two numbers.
        /// </summary>
        /// <param name="number1">The first number.</param>
        /// <param name="number2">The second number.</param>
        /// <returns>The sum of the two numbers.</returns>
        public double Add(double number1, double number2)
        {
            return number1 + number2;
        }

        /// <summary>
        /// Subtracts the second number from the first.
        /// </summary>
        /// <param name="number1">The first number.</param>
        /// <param name="number2">The second number.</param>
        /// <returns>The difference between the two numbers.</returns>
        public double Subtract(double number1, double number2)
        {
            return number1 - number2;
        }

        /// <summary>
        /// Multiplies two numbers.
        /// </summary>
        /// <param name="number1">The first number.</param>
        /// <param name="number2">The second number.</param>
        /// <returns>The product of the two numbers.</returns>
        public double Multiply(double number1, double number2)
        {
            return number1 * number2;
        }

        /// <summary>
        /// Divides the first number by the second.
        /// </summary>
        /// <param name="number1">The first number.</param>
        /// <param name="number2">The second number.</param>
        /// <returns>The quotient of the two numbers.</returns>
        public double Divide(double number1, double number2)
        {
            if (number2 == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero.");
            }
            return number1 / number2;
        }

        /// <summary>
        /// Calculates the power of a number.
        /// </summary>
        /// <param name="baseNumber">The base number.</param>
        /// <param name="exponent">The exponent.</param>
        /// <returns>The result of the base number raised to the exponent.</returns>
        public double Power(double baseNumber, double exponent)
        {
            return Math.Pow(baseNumber, exponent);
        }

        // Additional mathematical operations can be added here.
    }
}
