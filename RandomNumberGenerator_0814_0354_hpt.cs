// 代码生成时间: 2025-08-14 03:54:20
using System;
using System.Threading.Tasks;

namespace RandomNumberService
{
    /// <summary>
    /// This class is responsible for generating random numbers.
    /// </summary>
    public class RandomNumberGenerator
    {
        private static readonly Random _random = new Random();

        /// <summary>
        /// Generates a random integer between two specified numbers.
        /// </summary>
        /// <param name="minValue">The minimum value of the random number.</param>
        /// <param name="maxValue">The maximum value of the random number.</param>
        /// <returns>A random integer between minValue and maxValue.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when minValue is greater than maxValue.</exception>
        public int GenerateRandomNumber(int minValue, int maxValue)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentOutOfRangeException("minValue", "Minimum value cannot be greater than maximum value.");
            }

            return _random.Next(minValue, maxValue + 1);
        }

        /// <summary>
        /// Generates a random double between 0 and 1.
        /// </summary>
        /// <returns>A random double between 0 and 1.</returns>
        public double GenerateRandomDouble()
        {
            return _random.NextDouble();
        }
    }
}
