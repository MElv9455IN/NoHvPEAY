// 代码生成时间: 2025-08-18 19:10:40
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace AntiSqlInjectionExample
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class UsersContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }

    public class UserService
    {
        private readonly UsersContext _context;

        public UserService(UsersContext context)
        {
            _context = context;
        }

        // This method demonstrates how to use Entity Framework to safely query the database
        // and prevent SQL injection.
        public User FindUserByUsername(string username)
        {
            try
            {
                // Using EF's built-in methods to execute a parameterized query to prevent SQL injection.
                var user = _context.Users.FirstOrDefault(u => u.Username == username);
                return user;
            }
            catch (Exception ex)
            {
                // Log the exception and rethrow it to handle it at a higher level if necessary.
                Console.WriteLine("An error occurred while finding the user: " + ex.Message);
                throw;
            }
        }

        // This method demonstrates how to add a new user to the database,
        // which is also protected against SQL injection.
        public void AddUser(User newUser)
        {
            try
            {
                // Adding a new entity to the DbContext is safe against SQL injection.
                _context.Users.Add(newUser);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                // Handle specific database update exceptions.
                Console.WriteLine("An error occurred while adding the user: " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                // Log the exception and rethrow it to handle it at a higher level if necessary.
                Console.WriteLine("An error occurred: " + ex.Message);
                throw;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new UsersContext())
            {
                var userService = new UserService(context);

                // Example of finding a user by username.
                User user = userService.FindUserByUsername("exampleUser");
                if (user != null)
                {
                    Console.WriteLine("User found: " + user.Username);
                }
                else
                {
                    Console.WriteLine("User not found.");
                }

                // Example of adding a new user.
                User newUser = new User { Username = "newUser", Password = "newPassword" };
                userService.AddUser(newUser);
                Console.WriteLine("User added successfully.");
            }
        }
    }
}
