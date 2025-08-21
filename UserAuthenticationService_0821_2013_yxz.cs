// 代码生成时间: 2025-08-21 20:13:02
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

// Define the User class
public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
}

// Define the DbContext for the application
public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }
}

// Define the UserAuthenticationService class
public class UserAuthenticationService
{
    private readonly ApplicationContext _context;

    public UserAuthenticationService(ApplicationContext context)
    {
        _context = context;
    }

    /*
     * AuthenticateUser method checks if the provided username
     * and password match the stored credentials.
     * Returns true if the credentials are valid, false otherwise.
     */
    public bool AuthenticateUser(string username, string password)
    {
        try
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                // User not found
                return false;
            }

            // Verify hashed password
            if (VerifyPasswordHash(password, user.PasswordHash))
            {
                return true;
            }
            else
            {
                // Password is incorrect
                return false;
            }
        }
        catch (Exception ex)
        {
            // Log the exception details here
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
    }

    /*
     * HashPassword method generates a salted hash of the provided password.
     * It uses SHA256 hashing algorithm.
     */
    private string HashPassword(string password)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            // ComputeHash - returns byte array
            byte[] sourceBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = sha256Hash.ComputeHash(sourceBytes);

            // Convert byte array to a string
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                builder.Append(hashBytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }

    /*
     * VerifyPasswordHash method checks if the provided password matches
     * the stored password hash.
     */
    private bool VerifyPasswordHash(string password, string storedHash)
    {
        return HashPassword(password) == storedHash;
    }
}
