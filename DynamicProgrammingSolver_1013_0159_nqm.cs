// 代码生成时间: 2025-10-13 01:59:24
using System;
using System.Collections.Generic;
using System.Linq;

namespace DynamicProgrammingSolutions
{
    /// <summary>
    /// A class that provides solutions to dynamic programming problems.
    /// </summary>
    public class DynamicProgrammingSolver
    {
        /// <summary>
        /// Solves the classic Fibonacci sequence problem using dynamic programming.
        /// </summary>
        /// <param name="n">The position in the Fibonacci sequence to calculate.</param>
        /// <returns>The Fibonacci number at the specified position.</returns>
        public int Fibonacci(int n)
        {
            if (n < 0)
            {
                throw new ArgumentException("Input cannot be negative.", nameof(n));
            }

            Dictionary<int, int> memoization = new Dictionary<int, int>();
            return FibonacciHelper(n, memoization);
        }

        private int FibonacciHelper(int n, Dictionary<int, int> memoization)
        {
            if (n < 2) return n;
            if (memoization.ContainsKey(n)) return memoization[n];

            int result = FibonacciHelper(n - 1, memoization) + FibonacciHelper(n - 2, memoization);
            memoization.Add(n, result);
            return result;
        }

        /// <summary>
        /// Solves the 0/1 Knapsack problem using dynamic programming.
        /// </summary>
        /// <param name="values">The values of the items.</param>
        /// <param name="weights">The weights of the items.</param>
        /// <param name="capacity">The capacity of the knapsack.</param>
        /// <returns>The maximum value that can be carried in the knapsack.</returns>
        public int Knapsack(int[] values, int[] weights, int capacity)
        {
            if (values == null || weights == null || capacity < 0)
            {
                throw new ArgumentException("Invalid input for Knapsack problem.");
            }

            int n = values.Length;
            int[,] dp = new int[n + 1, capacity + 1];

            for (int i = 1; i <= n; i++)
            {
                for (int w = 1; w <= capacity; w++)
                {
                    if (weights[i - 1] <= w)
                    {
                        dp[i, w] = Math.Max(dp[i - 1, w], dp[i - 1, w - weights[i - 1]] + values[i - 1]);
                    }
                    else
                    {
                        dp[i, w] = dp[i - 1, w];
                    }
                }
            }

            return dp[n, capacity];
        }

        // Additional dynamic programming solutions can be added here.
    }
}
