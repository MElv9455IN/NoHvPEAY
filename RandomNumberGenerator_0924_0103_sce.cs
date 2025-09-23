// 代码生成时间: 2025-09-24 01:03:26
using System;
using System.Threading.Tasks;

namespace RandomNumberApp
{
    /// <summary>
    /// A class that generates random numbers.
    /// </summary>
    public class RandomNumberGenerator
    {
        private readonly Random _random;

        /// <summary>
        /// Initializes a new instance of the RandomNumberGenerator class.
        /// </summary>
        public RandomNumberGenerator()
        {
            _random = new Random();
        }

        /// <summary>
        /// Generates a random number within a specified range.
        /// </summary>
        /// <param name="minValue">The minimum value of the range.</param>
        /// <param name="maxValue">The maximum value of the range.</param>
        /// <returns>The generated random number.</returns>
        public int GenerateRandomNumber(int minValue, int maxValue)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentException("minValue cannot be greater than maxValue.");
            }
            return _random.Next(minValue, maxValue);
        }

        /// <summary>
        /// Asynchronously generates a random number within a specified range.
        /// </summary>
        /// <param name="minValue">The minimum value of the range.</param>
        /// <param name="maxValue">The maximum value of the range.</param>
        /// <returns>A Task representing the asynchronous operation, with the result being the generated random number.</returns>
        public async Task<int> GenerateRandomNumberAsync(int minValue, int maxValue)
        {
            return await Task.Run(() => GenerateRandomNumber(minValue, maxValue));
        }
    }
}
