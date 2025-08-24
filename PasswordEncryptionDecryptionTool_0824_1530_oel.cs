// 代码生成时间: 2025-08-24 15:30:10
using System;

using System.Security.Cryptography;

using System.Text;



/// <summary>
/// A utility class for encrypting and decrypting passwords.
/// </summary>
public class PasswordEncryptionDecryptionTool
{
    private readonly HashAlgorithm _hashAlgorithm;
    private readonly Encoding _encoding;

    /// <summary>
    /// Initializes a new instance of the PasswordEncryptionDecryptionTool class.
    /// </summary>
    public PasswordEncryptionDecryptionTool()
    {
        // Using SHA256 for hashing and UTF8 for encoding.
        _hashAlgorithm = SHA256.Create();
        _encoding = Encoding.UTF8;
    }

    /// <summary>
    /// Encrypts a password using SHA256 hashing algorithm.
    /// </summary>
    /// <param name="plainTextPassword">The plain text password to be encrypted.</param>
    /// <returns>The encrypted password as a string.</returns>
    public string EncryptPassword(string plainTextPassword)
    {
        if (string.IsNullOrWhiteSpace(plainTextPassword))
        {
            throw new ArgumentException("Plain text password cannot be null or whitespace.", nameof(plainTextPassword));
        }

        byte[] plainTextBytes = _encoding.GetBytes(plainTextPassword);
        byte[] hashedBytes = _hashAlgorithm.ComputeHash(plainTextBytes);
        return Convert.ToBase64String(hashedBytes);
    }

    /// <summary>
    /// Decrypts a password using SHA256 hashing algorithm.
    /// NOTE: Hashed passwords are not reversible, this method is a placeholder for
    /// verifying if the provided password matches the stored hash.
    /// </summary>
    /// <param name="hashedPassword">The hashed password to be verified.</param>
    /// <param name="plainTextPassword">The plain text password to compare with the hashed password.</param>
    /// <returns>True if the passwords match, otherwise false.</returns>
    public bool DecryptPassword(string hashedPassword, string plainTextPassword)
    {
        if (string.IsNullOrWhiteSpace(hashedPassword) || string.IsNullOrWhiteSpace(plainTextPassword))
        {
            throw new ArgumentException("Hashed and plain text passwords cannot be null or whitespace.", nameof(hashedPassword));
        }

        string encryptedHashedPassword = EncryptPassword(plainTextPassword);
        return encryptedHashedPassword == hashedPassword;
    }
}
