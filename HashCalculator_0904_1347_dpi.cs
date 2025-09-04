// 代码生成时间: 2025-09-04 13:47:35
 * A utility program that calculates hash values for given input strings using Entity Framework.
 * 
 * This program includes error handling and follows C# best practices for maintainability and expandability.
 */
using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace HashingTools
{
    public class HashCalculator
    {
        private readonly DbContext _context;

        public HashCalculator(DbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Calculates the hash value for a given input string.
        /// </summary>
        /// <param name="input">The input string to hash.</param>
        /// <returns>The hash value as a hexadecimal string.</returns>
        public string CalculateHash(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("Input string cannot be null or empty.", nameof(input));
            }

            using (SHA256 sha256 = SHA256.Create())
            {
                // Compute the hash of the input string.
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Convert the bytes to a hexadecimal string.
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        /// <summary>
        /// Verifies if the input string matches the provided hash.
        /// </summary>
        /// <param name="input">The input string to verify.</param>
        /// <param name="hash">The expected hash value.</param>
        /// <returns>True if the input matches the hash, otherwise false.</returns>
        public bool VerifyHash(string input, string hash)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(hash))
            {
                throw new ArgumentException("Input and hash cannot be null or empty.", nameof(input));
            }

            string calculatedHash = CalculateHash(input);
            return calculatedHash.Equals(hash, StringComparison.OrdinalIgnoreCase);
        }
    }
}
